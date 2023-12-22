
# Vehicle Market

This project is developed using .NET 7. It's an electronic vehicle marketplace simulation. Below, you'll find information on how to run the project along with technical specifications.



## Tech Stack

- .NET 7
- Docker & Docker-Compose
- PosgreSql
- RabbitMQ
- Dapper
- Automapper
- FluentValidation
- DepencyInjection
- Ngpsql
- MediatR
- Swagger

## Architectural

- Domain Driven Design Pattern
- Microservice
- Mediator Design Pattern
- CQRS Design Pattern
- Repository Design Pattern

  
## RUN

Clone

```bash

  git clone https://github.com/frtlec/VehicleMarket.git

```

Go Root directory

```bash

  cd VehicleMarket

```

Run Docker containers 
```bash

  docker-compose up -d

```



We can start partying when the Docker containers are up and running.

<br/>🥳😎😆🥳😎😆🥳😎😆🥳😎😆🥳😎😆🥳😎
<br/>🥳😎😆🥳😎😆🥳😎😆🥳😎😆🥳😎😆🥳😎


- If everything has gone smoothly so far, you should be able to reach the 'advert.api' from the link below



## Connect
  - [Advert Api (:7100)](http://localhost:7100/swagger/index.html)
  - [Rabbitmq (:15673)](http://localhost:15673/) guest:guest


## Connect Postgre SQL
- HOST=localhost:5432
- DB=advertdb
- USERID=admin
- PASS=Password12*
- "ConnectionStrings:PostgreSql=User ID=admin; Password=Password12*; Server=advertdb; Port=5432;Database=advertdb;Integrated Security=true;Pooling=true"
  


## Notes !
- The initial data and tables of the project come from the init.sql file.
- init.sql is called in the docker-compose.override.yml file. (look ./docker-compose.override.yml)

## Using Advert API 

#### Get All
This method, It helps to filter and page all adverts in the system.
```http

  GET /advert/all

```
** It can be left empty; when left blank, the filter won't be included in the search.
| Parameters | Type     | Description  | Data |
| :-------- | :------- | :-------------------------------- | :------------ |
| `CategoryId`| `int` | Advert category filter.|
| `BeginPrice`| `decimal` | Price range start.|
| `EndPrice`| `decimal` | Price range end.|
| `Gear`| `string` | Gear filter.|
| `Fuel`| `string` | Fuel filter.|
| `Take`| `int` | **Required**, Can be used for pagination.| 0 for all |
| `Skip`| `int` | **If Take is 0 it will not work**, Can be used for pagination.|
| `Sort`| `array(sortmodel)` | It can be used for sorting, we can sort by multiple fields|Sort[0].ColumnName=(price-year-km) Sort[0].Directive=(ASC-DESC )|


#### Get
This method advert information in detail
```http

  GET /advert/get?id=${id}

```

| Parameters | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. The ID field of the item to be called.|

#### Record Visit 
This method saves the advertisements shown by the client application to the user in the database according to the IP address and visit date information.
```http

  POST /advert/get

```
content-type:application/json

| Parameters | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `advertId`      | `int` | **Required**. Advert ID number|


  
