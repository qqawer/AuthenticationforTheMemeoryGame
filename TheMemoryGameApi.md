---
title: 默认模块
language_tabs:
  - shell: Shell
  - http: HTTP
  - javascript: JavaScript
  - ruby: Ruby
  - python: Python
  - php: PHP
  - java: Java
  - go: Go
toc_footers: []
includes: []
search: true
code_clipboard: true
highlight_theme: darkula
headingLevel: 2
generator: "@tarslib/widdershins v4.0.30"

---

# 默认模块

Base URLs:

* <a href="http://localhost:5011/api/">TheMemoryGame: http://localhost:5011/api/</a>

# Authentication

- HTTP Authentication, scheme: bearer

# TheMemoryGame

## POST Userlogin

POST /Auth/login

> Body Parameters

```json
{
  "username": "user",
  "password": "123456"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|object| no |none|

> Response Examples

> 200 Response

```json
{
  "code": 200,
  "message": "Login successful.",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXIiLCJVc2VySWQiOiIxIiwiSXNQYWlkIjoiRmFsc2UiLCJuYmYiOjE3NjYzMzI1MDYsImV4cCI6MTc2NjMzMjUwNiwiaWF0IjoxNzY2MzMyNTA2LCJpc3MiOiJTQTYxIiwiYXVkIjoiU0E2MS0wMSJ9.TeH6SFN0ewg77dcQPiEapEYh6eEhx7vVcP0TuHSxAxo"
  }
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|none|Inline|

### Responses Data Schema

HTTP Status Code **200**

|Name|Type|Required|Restrictions|Title|description|
|---|---|---|---|---|---|
|» code|integer|true|none||none|
|» message|string|true|none||none|
|» data|object|true|none||none|
|»» token|string|true|none||none|

## POST SubmitScore

POST /Score/submit

> Body Parameters

```json
{
  "completionTimeSeconds": 2147483647
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|object| no |none|
|» completionTimeSeconds|body|integer| yes |none|

> Response Examples

> 200 Response

```json
{
  "code": 200,
  "message": "Score submitted successfully.",
  "data": null
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|none|Inline|

### Responses Data Schema

HTTP Status Code **200**

|Name|Type|Required|Restrictions|Title|description|
|---|---|---|---|---|---|
|» code|integer|true|none||none|
|» message|string|true|none||none|
|» data|null|true|none||none|

## GET Getleaderboard

GET /Score/leaderboard

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|page|query|string| no |none|
|size|query|string| no |none|

> Response Examples

> 200 Response

```json
{
  "code": 200,
  "message": "Leaderboard retrieved successfully.",
  "data": {
    "items": [
      {
        "username": "vip",
        "completeTimeSeconds": 60,
        "completeAt": "2025-12-20T23:02:24.373"
      },
      {
        "username": "user",
        "completeTimeSeconds": 95,
        "completeAt": "2025-12-20T22:32:24.373"
      },
      {
        "username": "user1",
        "completeTimeSeconds": 200,
        "completeAt": "2025-12-19T23:02:24.373"
      }
    ],
    "totalCount": 3,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|none|Inline|

### Responses Data Schema

HTTP Status Code **200**

|Name|Type|Required|Restrictions|Title|description|
|---|---|---|---|---|---|
|» code|integer|true|none||none|
|» message|string|true|none||none|
|» data|object|true|none||none|
|»» items|[object]|true|none||none|
|»»» username|string|true|none||none|
|»»» completeTimeSeconds|integer|true|none||none|
|»»» completeAt|string|true|none||none|
|»» totalCount|integer|true|none||none|
|»» pageNumber|integer|true|none||none|
|»» pageSize|integer|true|none||none|
|»» totalPages|integer|true|none||none|

## GET GetactiveAds

GET /Ad/active

> Response Examples

> 200 Response

```json
{
  "code": 200,
  "message": "Active ads retrieved successfully.",
  "data": [
    {
      "id": 1,
      "adTitle": "dog1",
      "adImageUrl": "http://localhost:5011/images/ads/ad1.png",
      "isActive": true
    },
    {
      "id": 2,
      "adTitle": "dog2",
      "adImageUrl": "http://localhost:5011/images/ads/ad2.png",
      "isActive": true
    },
    {
      "id": 3,
      "adTitle": "dog3",
      "adImageUrl": "http://localhost:5011/images/ads/ad3.png",
      "isActive": true
    }
  ]
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|none|Inline|

### Responses Data Schema

HTTP Status Code **200**

|Name|Type|Required|Restrictions|Title|description|
|---|---|---|---|---|---|
|» code|integer|true|none||none|
|» message|string|true|none||none|
|» data|[object]|true|none||none|
|»» id|integer|true|none||none|
|»» adTitle|string|true|none||none|
|»» adImageUrl|string|true|none||none|
|»» isActive|boolean|true|none||none|

# Data Schema

