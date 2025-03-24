namespace RevCompany.Infrastructure.Persistence.user;

public static class CreateTable
{
  public static string SQLTemplate =  @"
    CREATE TABLE IF NOT EXISTS users(
      id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
      first_name VARCHAR(255) NOT NULL,
      last_name VARCHAR(255) NOT NULL,
      email VARCHAR(255) NOT NULL UNIQUE,
      password VARCHAR(255) NOT NULL
    );
  " ;
}
