# ASP_b9-Safi

In the program.cs we configure the email confuguration and to work properly we  created a custom class .For  MSSql server and File Sink we read the configuration from appsettings.json .For Email the minimum log level set to Fatal and others log level is verbose which is the lowest level .

To Test By your own with your gmail follow this proccess :

1 . go to this link https://myaccount.google.com/security

2.  Then you have to on the 2 step verification if already you have not done this yet .

3. You have to create a app in the bottom section name "App Passwords".You should save the password some where else because you will see this only one time . 

4. Replace the userName and password with yours in the programm.cs .