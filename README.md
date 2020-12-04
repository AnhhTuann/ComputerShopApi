## How to use API

`GET /api/category`

> Get all categories

**Response:**

```json
[
    {
        "id": 1,
        "name": "CPU"
    },
    {
        "id": 2,
        "name": "RAM"
    }
]
```

---

`GET /api/category/:id`

> Get a category object by its `id`

**Response:**

```json
{
    "id": 1,
    "name": "CPU"
}
```

---

`POST /api/category`

> Create new category object

**Body:**

```json
{
    "name": "Mainboard"
}
```

---

`GET /api/product`

> Get all products

**Response:**

```json
[
    {
        "id": 1,
        "name": "AMD RX550",
        "price": 1000,
        "amount": 5,
        "category": {
            "id": 1,
            "name": "VGA",
        },
        "image": "741aqwd54dqw"
    },
    {
        "id": 2,
        "name": "Intel Core i5 7400",
        "description": "Powerful CPU",
        "price": 2000,
        "amount": 4,
        "category": {
            "id": 2,
            "name": "CPU",
        },
        "image": "741aqwd5wd54"
    }
]
```

---

`GET /api/product/:id`

> Get a product object by its `id`

**Response:**

```json
{
    "id": 1,
    "name": "AMD RX550",
    "price": 1000,
    "amount": 5,
    "category": {
        "id": 1,
        "name": "VGA",
    },
    "image": "741aqwd54dqw"
}
```

---

`POST /api/product`

> Create new category object

**Body:**

Using `Multipart Form Data`

---

`GET /api/image/:name`

> Using with `<img>` tag to get product image.

**Response:**

`image/png`

---

`GET /api/storage/`

> Get all import/export tickets

**Response:**

```json
[
    {
        "id": 1,
        "product": {
            "id": 1,
            "name": "AMD RX550",
            "price": 1000,
            "amount": 5,
            "category": {
                "id": 1,
                "name": "VGA",
            },
            "image": "741aqwd54dqw"
        },
        "import": 0,
        "export": 1,
        "date": "04/12/2020"
    },
    {
        "id": 1,
        "product": {
            "id": 1,
            "name": "AMD RX550",
            "price": 1000,
            "amount": 5,
            "category": {
                "id": 1,
                "name": "VGA",
            },
            "image": "741aqwd54dqw"
        },
        "import": 5,
        "export": 0,
        "date": "04/12/2020"
    }
]
```

---

`GET /api/storage/:id`

> Get import/export ticket by its `id`

**Response:**

```json
{
    "id": 1,
    "product": {
        "id": 1,
        "name": "AMD RX550",
        "price": 1000,
        "amount": 5,
        "category": {
            "id": 1,
            "name": "VGA",
        },
        "image": "741aqwd54dqw"
    },
    "import": 0,
    "export": 1,
    "date": "04/12/2020"
}
```

---

`POST /api/storage`

> Create new import/export ticket and also alter the amount of the specified product.

**Body:**

```json
// updating...
```

---
