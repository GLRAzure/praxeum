using HtmlAgilityPack;
using ScrapySharp.Extensions;
using Praxeum.FunctionApp.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Praxeum.FunctionApp.Helpers
{
    public class MicrosoftProfileScraper
    {
        public const string DISPLAY_NAME_SELECTOR = "#profile-section nav .title";
        public const string USER_NAME_SELECTOR = "#profile-section .card-content > div > div.is-size-7";
        public const string CREATED_ON_SELECTOR = "#profile-section .card-content .subtitle > span";
        public const string BADGES_SELECTOR = "[data-bi-name='badges'] .achievement-card .card-content";
        public const string BADGE_IMAGE_SELECTOR = "figure > img";
        public const string BADGE_TITLE_SELECTOR = "h3 > a";
        public const string BADGE_ACHIEVED_DATE_SELECTOR = "nav .level-left .subtitle";
        public const string LEVEL_SELECTOR = ".card > .columns > .column.is-one-third .title.is-size-1";
        public const string EXPERIENCE_SELECTOR = ".card > .columns > .column.is-one-third div > p.is-size-8";

        public Learner FetchProfile(
             string userPrincipalName)
        {
            var learner =
                new Learner();

            var web = new HtmlWeb();
            var doc = web.Load("https://techprofile.microsoft.com/en-us/" + userPrincipalName);

            var html = doc.DocumentNode;

            IEnumerable<HtmlNode> nodes;

            nodes =
                html.CssSelect(DISPLAY_NAME_SELECTOR);

            learner.DisplayName =
                nodes.First().InnerText;

            nodes =
                html.CssSelect(USER_NAME_SELECTOR);

            learner.UserName =
                nodes.First().InnerText;

            nodes =
                html.CssSelect(CREATED_ON_SELECTOR);

            foreach (var node in nodes)
            {
                var createdOn =
                    node.InnerText;
                createdOn =
                    createdOn.Replace("Member Since", "");

                learner.CreatedOn = DateTime.Parse(createdOn);
            }

            nodes =
                html.CssSelect(BADGES_SELECTOR);

            foreach (var node in nodes)
            {
                var achievement =
                    new LearnerAchievement();

                achievement.Type = "badge";

                foreach (var childNode in node.CssSelect(BADGE_IMAGE_SELECTOR))
                {
                    achievement.Image =
                        childNode.Attributes["src"].Value;
                }

                foreach (var childNode in node.CssSelect(BADGE_TITLE_SELECTOR))
                {
                    achievement.Url =
                        childNode.Attributes["href"].Value;
                    achievement.Title =
                        childNode.InnerText;
                }

                foreach (var childNode in node.CssSelect(BADGE_ACHIEVED_DATE_SELECTOR))
                {
                    achievement.AchievedDate =
                        DateTime.Parse(childNode.InnerText);
                }

                learner.Achievements.Add(achievement);
            }

            nodes =
                html.CssSelect(LEVEL_SELECTOR);

            learner.ProgressStatus.CurrentLevel =
                int.Parse(nodes.First().InnerText);

            nodes =
                html.CssSelect(EXPERIENCE_SELECTOR);

            var experience =
                nodes.First().InnerText;
         
            experience =
                experience.Replace("XP", "").Trim();

            learner.ProgressStatus.CurrentLevelPointsEarned =
                int.Parse(experience.Split('/')[0]);
            learner.ProgressStatus.TotalPoints =
                int.Parse(experience.Split('/')[1]);

            learner.ProgressStatus.BadgesEarned =
                learner.Achievements.Count(x => x.Type == "badge");
            learner.ProgressStatus.TrophiesEarned =
                learner.Achievements.Count(x => x.Type == "trophy");

            return learner;
        }
    }
}
