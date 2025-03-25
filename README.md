# Recompany
<h3> This project aims to create a sales management </h3>
<p>
  <img alt="C#" src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white"/>
  <img alt="angular" src="https://img.shields.io/badge/A-AngularJs-red" />
</p>
<br/>

# Pré-Requisito
<h4>Docker: https://www.docker.com/products/docker-desktop</h4>
<h4>Node: https://nodejs.org/en/download</h4>

# You can check the full documentation navigating to 
### doc: [https://github.com/brunoo-p/RevCompany/tree/main/doc]
<br/>

# Starting 🚀
<h4>Clone the repositóry</h4>
```bash
git clone https://github.com/bunoo-p/RrevCompany.git
cd revcompany
```

## Building 🔧⚙
```shel
cd Storage [root folder] 
docker-compose up -d
```

## Stopping 🔧⚙
```shel
docker-compose down
```

- The command ``` docker-compose up -d ``` will start 4 containers:

  - Postgres: Postgres Database at ``localhost: 5432``
      - ``ports: 5432:5432``
      
  - Pgadmin : Postgres interface at ```localhost:5050```
      - ``ports: 5050:80``
      - sign-  ``email: email@email.com | password: admin``
      - create server
      - Add new Server
      - General > ``Name: server``
      - Connection > ``
          Host name/address: posetgres_container
          Username: postgres
          Password: yourpassword
          ``
      
  - API: WebApi em AspNetCore version=8.0  HTTP ``localhost:5117`` 
      - ``ports: 5117:80``
      - Swagger in  [ localhost: 5117/swagger/index.html ]
  
  - UI: Angular ``localhost: 4200``
      - ``ports: 4200:4200``

## Pipeline CI/CD
  ### Jobs:
    - build backend application
    - run unit tests

