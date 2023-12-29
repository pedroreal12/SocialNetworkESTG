# SocialMediaESTG
This project was done in the context of Web Programming class

## Project objectives
- Develop a social networking platform centered around movies using .NET MVC, allowing users to connect, discuss, and share their favorite movies and reviews.
- Integrate a movie database/API (TMDb) to fetch movie information, including titles, posters, release dates, genres, etc.

### Main project features
- Users can create and manage their profiles.
- Enable users to create lists (e.g., favorites, watchlist) to organize and save movies.
- Users can rate and review movies.
- Implement a rating system (e.g., star ratings) for movies.
- Display aggregated ratings and reviews for each movie.
- Implement a news feed showing updates from users, such as reviewed movies, comments, etc.

### Technical Requirements for the project
- Utilize .NET MVC for server-side development. Implement Model-View-Controller architecture.
- Use a SQL Server database to store user data, movie details, reviews, comments, etc. Design an appropriate database schema for storing movie-related information.
- Use HTML, CSS, JavaScript for frontend UI/UX.
- Utilize Razor syntax and ASP.NET MVC views for rendering.
- Utilize [The Movie Database APIs](https://www.themoviedb.org) to fetch additional movie-related data, such as trailers, cast details, or related news.

### How to setup this project
1. Install the following libraries in your visual studio (Have in mind that I'm using the SDK .NET 6.0 to run this project locally and in my Docker container:
    - Microsoft.EntityFrameworkCore.Tools Version Version 7.0.13
    - Microsoft.EntityFrameworkCore.SqlServer Version 7.0.13
    - Microsoft.AspNetCore.Identity.UI Version 6.0.23
    - Microsoft.AspNetCore.Identity.EntityFrameworkCore Version 6.0.23
    - Microsoft.VisualStudio.Web.CodeGeneration.Design Version 6.0.16
    - RestSharp Version 110.2.0
2. Build and run the docker container with the command "docker-compose up --build"
3. Connect in the SQL Server with the credentials in the compose.yaml file (Note that the address and port should be separate by a ",")
4. Run the file scriptDB.sql inside the SQL Server (This SQL script will create two databases "sndb" and "snidentitydb", "sndb" being the main database and "snidentitydb" will be used by identity framework
5. Now go onto the nuget package manager console in visual studio and run the following command "Scaffold-DbContext "Your connection string" Microsoft.EntityFrameworkCore.SqlServer -Outputdir Models"
    - Your connection string should be something like this: "Server=localhost, 1433;Database=sndb;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=True;MultipleActiveResultSets=true"
    - Have in mind that if you have an SQL Server instance running or any SQL Service service at all you might need to shutdown thoses services down as they will most likely be using the port 1433
6. Open and file appsettings.json and:
    - Add a value with the ConnectionString that you just used
    - Add another value with a connectrion string for you identity database
7. Add these connectionStrings to your Program.cs
    - Have a look at my [Program.cs](https://github.com/pedroreal12/SocialNetworkESTG/blob/main/SocialNetworkMovies/Program.cs) to have a better idea of how it should look like
8. Setup identity
    - Inside Visual Studio right click on the project file name 
    - Go to "Add"
    - Click on New Scaffolded Item
    - Choose Identity
    - You can choose the files you wish to use or even just override all files
    - Then click on the "+" icon at DbContext class and create a new class. This class will be used in your identity, then the same for your database provider and User class
    - Open the class file that you just created wich should be in Areas/Identity/Data (in my case is the class SocialNetworkMoviesContext) and create a constructor for this class with the same name the class has
9. Press the play button and you should be good to go with your development
