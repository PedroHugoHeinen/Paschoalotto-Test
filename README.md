# Paschoalotto Test
Project for admission test at Paschoalotto Serviços Financeiros S/A

## Installation

Configure project

```
• Restore backup "paschoalotto-base" in PostgreSQL
• Configure the connection string in the file "appsettings.json"
• Run in projects API and Web
```
![image](https://github.com/user-attachments/assets/4c79a423-5d2b-4161-80dd-19f390bddcff)

## Tech Stack

>**Front-end**

![HTML5](https://img.shields.io/badge/HTML5-f26327?style=for-the-badge)
![CSS3](https://img.shields.io/badge/Bootstrap-2465f1?style=for-the-badge)
![JS](https://img.shields.io/badge/JavaScript-f7df1e?style=for-the-badge)

>**Back-end**

![.Net Core Web API](https://img.shields.io/badge/.Net_Core-Web_API_RESTFUL-9a70d2?style=for-the-badge)

>**Database**

![PostgreSQL](https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge)

>**API Integration**

[![RandomAPI](https://img.shields.io/badge/RandomAPI-79b039?style=for-the-badge)](https://randomapi.com/documentation)

## Endpoint Flow

`Client` > `Controller` > `Service` > `Repository`

## API Reference

#### Get all users
```https
  GET /PaschoalottoApi/User/GetAll
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|  |  | Simple call |

#### Get user by id
```https
  GET /PaschoalottoApi/User/GetById/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of user to fetch |

#### Insete user
```https
  POST /PaschoalottoApi/User/Insert/{UserDTO}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `UserDTO`      | `JSON` | **Required**. User to inserted |

#### Update user
```https
  PUT /PaschoalottoApi/User/Update/{UserDTO}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `UserDTO`      | `JSON` | **Required**. User to updated |

#### Delete user by id
```https
  DELETE /PaschoalottoApi/User/Delete/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of user to delete |

#### Insert users from randomuser.me
```https
  GET /PaschoalottoApi/User/InsertRandom
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|  |  | Simple call. Service will import 10 users at a time | 

#### Generates report (Spreadsheet) with all users
```https
  GET /PaschoalottoApi/User/GenerateReport
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|  |  | Simple call. service will generate the report and return the spreadsheet to download | 
