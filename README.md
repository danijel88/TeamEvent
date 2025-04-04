# Usage
* First need in appsettings.json for prod env to set connection string `"Default": "server=DESKTOP-DURJP2A\\SQLEXPRESS; database=events;user id=sa;password=sqladmin;TrustServerCertificate=true"`
*  for development to use `appsettings.Development.json`
* After that from Visual Studio Package Manager need to run command: `update-database -p teamevents.server`
or from Visual code `dotnet ef database update`
* In Frontend project find `vite.config.ts` there is variable tenantId which is used for api header.
* Also there is configuration for server and ports
`const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7019';`
After that application can be run from IDE.
