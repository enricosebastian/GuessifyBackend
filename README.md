# Set up
## Prerequisites
- Use Visual Studio 2022 (or higher)

## Steps
- Clone the repository `git clone git@github.com:enricosebastian/GuessifyBackend.git`
- Move to the main folder `cd GuessifyBackend`
- Make sure you are using the master/main branch `git fetch && git reset --hard origin/master && git clean -fd`
- In Visual Studio, build the project
- Run the web application 

# Details
This backend web application is currently hosted up on Azure via the Azure Web App service. Sample consumable API endpoints
- https://datacom-backend-job-app-hfb5e6cfa2achxbj.canadacentral-01.azurewebsites.net/RestCountries/GetAll
- https://datacom-backend-job-app-hfb5e6cfa2achxbj.canadacentral-01.azurewebsites.net/Agify/Get?name=enrico
- https://datacom-backend-job-app-hfb5e6cfa2achxbj.canadacentral-01.azurewebsites.net/AgifyAndGenderize/Get?name=enrico&countryId=PH
