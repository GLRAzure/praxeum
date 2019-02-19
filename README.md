# Cloud Skills Challenges #
This application was designed to help internal Microsoft employees to core enable customer developers through gamification using the Microsoft Learn portal.

Learner updates are based on the user's Microsoft Learn portal profile which can be found at https://techprofile.microsoft.com. 

The way developers are enabled is through contests, currently (5) different contests are supportd.

**Leaderboard**

This is a traditional leaderboard contest where users are ranked by total number of experience points accumulated.

**Total Points**

This contest is for learners trying to achieve a designated number of experience points. It is similar to the Leaderboard, except it has a definitive end. 

For this contest, all users begin with a starting value of 0.

**Total Levels**

This contest is similar to the Total Points contest, except it focuses on levels as opposed to points.

**Accumulated Points**

This contest is for learners trying to achive a specificed number of experience points based on their current points and a predefined total number of points to achieve.

The starting level is designated by the contest creator, and or the initial load of a learner.

This is a great contest to support initiatives, such as, the first to achieve 5,000 points, or some other designation.

**Accumulated Levels**

This is similar to the Accumulated Points contest, except it focuses on levels.

There is a public board for each contest, as well as a self-registration page. The links can be found on the Contests Details or the Contest List.

![Contest List Screenshot][screenshot-contest-list]

You can also customize the CSS for each public board and self-registration page through the `CustomCss` property on the contest.

![Contest Details Screenshot][screenshot-contest-details-public]

## Tools, Technologies and Languages ##

The following tools, technologies and languages were used to construct the application.

* C#, HTML and JavaScript
* ASP.NET Core 2.2
* ASP.NET MVC Razor Pages
* Azure App Services
* Azure Functions
* Azure CosmosDB
* Azure Table Storage
* Azure Storage Queues
* Azure AppInsights
* Azure ADB2C

[screenshot-contest-list]: https://github.com/GLRAzure/praxeum/blob/master/screenshot-contest-list.png?raw=true
[screenshot-contest-details-public]: https://github.com/GLRAzure/praxeum/blob/master/screenshot-contest-details-public.png?raw=true

## Release Notes ##

**1.0.0

* Support Contest management.
* Support Learner management for a Contest.

**1.1.0

* Support multiple types of contests: Leaderboard, Total Point, Total Levels, Accumulated Points and Accumulated Levels.
* Customized public contest pages.
* Export of contest learner results from the contest details page.


