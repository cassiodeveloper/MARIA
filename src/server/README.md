# EN-US

# Development Environment

What you will need
- Docker
- .NET SDK Version: 8.0.400
- EF tools 
	```dotnet tool install --global dotnet-ef```

# How to run

- First, you need to start the database:
```docker run --name db-postgres -p 5432:5432 -e POSTGRES_PASSWORD=admin -e POSTGRES_USER=admin -d postgres```

- Update database
```dotnet ef database update --startup-project .\Maria.Rest\ --project .\Maria.Infrastructure\ ```

- Then, you can run the project:
```dotnet run```

- In the Maria.Rest.http file, you will find request examples.

## Security Considerations

- Generate a key or use an SSL certificate to run your project in production environments:
```openssl rand -base64 64```
- Place this key in the config or secret: MARIA_SECRET_IDENTITY.
- In the configuration guide, you can set the JWT expiration time using the key: ExpireInSeconds.