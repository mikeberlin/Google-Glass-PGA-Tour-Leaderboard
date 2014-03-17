Google-Glass-PGA-Tour-Scores
============================

Simple Google Glass app that displays the current PGA Tour schedule and their leaderboard (assuming the tournament has started).

Utilizes the Scores and Stats API provided by http://www.sportsdatallc.com. Go there and obtain an API Key to use this app. Or you can use the static files inclued in the assets directory if you want to use some sample data.

If you have obtained an API Key from Sports Data make sure you create an api_key.xml file in your Assets directory and set the file's Build Action to AndroidAsset. The xml file should bein the following format:

<?xml version="1.0" encoding="UTF-8" ?>
<sportsdata apikey="put-your-api-key-here"></sportsdata>

Thanks for checking this out and let me know if you all have any questions!

@mikeberlin