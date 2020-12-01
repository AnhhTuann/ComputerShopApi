## How to use API

`GET /api/category`

> Get all categories

**Response:**

```json
[
    {
        id: 1,
        name: "CPU"
    },
    {
        id: 2,
        name: "RAM"
    }
]
```

---

`GET /api/category/:id`

> Get a category object by its `id`

**Response:**

```json
{
    id: 1,
    name: "CPU"
}
```

---

`POST /api/category`

> Create new category object

**Body:**

```json
{
    name: "Mainboard"
}
```

---

`GET /api/product`

> Get all products

**Response:**

```json
// updating...
```

---

`GET /api/product/:id`

> Get a product object by its `id`

**Response:**

```json
// updating...
```

---



`POST /api/product`

> Create new category object

**Body:**

```json
// updating
```

---


