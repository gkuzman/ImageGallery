# ImageGallery
This app is made so the visitor can vote on 10 pictures from the gallery. Votes are saved once the user has submitted 10 votes. The user can change his vote at any time during voting period, but he/she shoud be aware that once the last vote has been submitted, the user will be redirected to the summary page, with an overview of every pictured voted on by user. You cannot change the votes after that period.

The voting is session based, meaning the visitor can vote once again if he/she closes browser (not tab!) and visits the page again.

If less than 10 votes are cast, they will be not saved in the database.

# Running locally

Clone or download the solution to your local drive.
SEE [WARNING](https://github.com/gkuzman/ImageGallery/blob/master/README.md#warning)

## Prerequisites
1) Docker for windows installed on the PC with linux containers. Running ofc.
2) File sharing enabled for the drive the solution has been cloned/downloaded to. To enable file sharing go to docker desktop -> settings    -> resources -> file sharing section.

## Running
Open up your favorite command prompt (cmd, powershell...) and navigate to the folder where the solution has been downloaded to. The folder should contain docker-compose.yml file in it. Follow the instructions below:

### First time run
Type 
`docker-compose up --build -d`
in your cmd and execute the command. 

### Subsequent runs
Type 
`docker-compose up -d` 
in your cmd and execute the command. 

### Shutting down without losing data in the databases (user votes)
Type 
`docker-compose stop`
in your cmd and execute the command.

### Shutting down and lose every piece of data (user votes)
Type 
`docker-compose down`
in your cmd and execute the command.

### App url
https://localhost:5000/


# Warning
It may take a while until everything required has been downloaded and built up.
When you see the output in the cmd that everything is done, you may need to wait a bit before navigating to the web app, since it takes some time to seed the necessary data and build up databases.

You can close the console window once everything is set up and the app will still run
