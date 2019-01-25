using HtmlAgilityPack;
using ScrapySharp.Extensions;
using Praxeum.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Helpers
{
    public class MicrosoftProfileScraper : IMicrosoftProfileScraper
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

        public async Task<MicrosoftProfile> FetchProfileAsync(
             string userPrincipalName)
        {
            var microsoftProfile =
                new MicrosoftProfile();

            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync("https://techprofile.microsoft.com/en-us/" + userPrincipalName);

            var html = doc.DocumentNode;

            IEnumerable<HtmlNode> nodes;

            nodes =
                html.CssSelect(DISPLAY_NAME_SELECTOR);

            microsoftProfile.DisplayName =
                nodes.First().InnerText;

            nodes =
                html.CssSelect(USER_NAME_SELECTOR);

            microsoftProfile.UserName =
                nodes.First().InnerText;

            nodes =
                html.CssSelect(CREATED_ON_SELECTOR);

            foreach (var node in nodes)
            {
                var createdOn =
                    node.InnerText;
                createdOn =
                    createdOn.Replace("Member Since", "");

                microsoftProfile.CreatedOn = DateTime.Parse(createdOn);
            }

            nodes =
                html.CssSelect(BADGES_SELECTOR);

            foreach (var node in nodes)
            {
                var achievement =
                    new MicrosoftProfileAchievement();

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

                microsoftProfile.Achievements.Add(achievement);
            }

            nodes =
                html.CssSelect(LEVEL_SELECTOR);

            microsoftProfile.ProgressStatus.CurrentLevel =
                int.Parse(nodes.First().InnerText);

            nodes =
                html.CssSelect(EXPERIENCE_SELECTOR);

            var experience =
                nodes.First().InnerText;
         
            experience =
                experience.Replace("XP", "").Trim();

            microsoftProfile.ProgressStatus.CurrentLevelPointsEarned =
                int.Parse(experience.Split('/')[0]);
            microsoftProfile.ProgressStatus.TotalPoints =
                int.Parse(experience.Split('/')[1]);

            microsoftProfile.ProgressStatus.BadgesEarned =
                microsoftProfile.Achievements.Count(x => x.Type == "badge");
            microsoftProfile.ProgressStatus.TrophiesEarned =
                microsoftProfile.Achievements.Count(x => x.Type == "trophy");

            return microsoftProfile;
        }
    }
}
