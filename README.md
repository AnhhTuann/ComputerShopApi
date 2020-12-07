# How to use API

This file contains description of all of APIs. Please read it carefully.

# Table of contents

1. [Category](#category)
   1. [Get All](#get-apicategory)
   2. [Get By ID](#get-apicategoryid)
   3. [Create New](#post-apicategory)
   4. [Update](#put-apicategory)
2. [Product](#product)
   1. [Get All](#get-apiproduct)
   2. [Get By ID](#get-apiproductid)
   3. [Create New](#post-apitproduct)
   4. [Update](#put-apiproduct)
3. [Storage (WIP)](#storage)
4. [Customer](#customer)
   1. [Get All](#get-apicustomer)
   2. [Get By ID](#get-apicustomerid)
   3. [Register](#post-apicustomer)
   4. [Login](#post-apilogincustomer)
5. [Combo](#combo)
   1. [Get All](#get-apicombo)
   2. [Get By ID](#get-apicomboid)
   3. [Create New](#post-apicombo)

# Category

## `GET /api/category`

> Get all categories

**Queries:**

| **Key**  | **Type** | **Description**          |
| -------- | -------- | ------------------------ |
| `search` | `string` | Search items by its name |

*Note: with `search` query, DON'T put search string in `""`.*

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

## `GET /api/category/:id`

> Get a category object by its `id`

**Response:**

```json
{
    "id": 1,
    "name": "CPU"
}
```

---

## `POST /api/category`

> Create new category object

**Body:**

```json
{
    "name": "Mainboard"
}
```

**Response:**

A newly created object `id`

```json
1
```

---

## `PUT /api/category`

> Update category object

**Body:**

```json
{
    "id": 1,
    "name": "CPU"
}
```

---

# Product

## `GET /api/product`

> Get all products

**Queries:**

| **Key**    | **Type** | **Description**                                                            |
| ---------- | -------- | -------------------------------------------------------------------------- |
| `search`   | `string` | Search items by its name, in its description and also in its category name |
| `category` | `number` | Search items by category                                                   |
| `minPrice` | `number` | Search items by lowest price                                               |
| `maxPrice` | `number` | Search items by highest price                                              |

*Note: with `search` query, DON'T put search string in `""`.*

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

## `GET /api/product/:id`

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

## `POST /api/product`

> Create new product

**Body:**

Using `Multipart Form Data`.

| **key**          | **type** |
| ---------------- | -------- |
| `name`           | `string` |
| `price`          | `number` |
| `amount`         | `number` |
| `category[id]`   | `number` |
| `category[name]` | `string` |
| `productImage`   | `file`   |

*Note: `category[name]` is optional.*

**Response:**

A newly created object `id`

```json
1
```

---

## `PUT /api/product`

> Update product information

**Body:**

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

## `GET /api/image/:name`

> Using with `<img>` tag to get product image.

**Response:**

`image/png`

---

# Storage

**Warning: these APIs haven't been completed yet, don't use them!**

## `GET /api/storage/`

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

## `GET /api/storage/:id`

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

## `POST /api/storage`

> Create new import/export ticket and also alter the amount of the specified product

**Body:**

```json
// updating...
```

**Response:**

A newly created object `id`

```json
1
```

---

# Customer

## `POST /api/customer`

> Create new customer account

**Body:**

```json
{
    "name": "Phan Dung Tri",
    "email": "phandungtri@email.com",
    "password": "Abcd1234",
    "address": "HCMC",
    "phone": "0326123456"
}
```

**Response:**

A newly created object `id`

```json
1
```

---

## `GET /api/customer`

> Get all information of customers

**Response:**

```json
[
    {
        "name": "Phan Dung Tri",
        "email": "phandungtri@email.com",
        "password": "Iamhashed",
        "address": "HCMC",
        "phone": "0326123456"
    },
    {
        "name": "Nguyen Quoc Vu",
        "email": "quocvu@email.com",
        "password": "Iamhashed",
        "address": "HCMC",
        "phone": "0123456789"
    }
]
```

---

## `GET /api/customer/:id`

> Get information of customer by `id`

**Response:**

```json
{
    "name": "Phan Dung Tri",
    "email": "phandungtri@email.com",
    "password": "Abcd1234",
    "address": "HCMC",
    "phone": "0326123456"
}
```

---

## `POST /api/login/customer`

> Perform a login action for a customer account

**Body:**

```json
{
    "email": "abcd@email.com",
    "password": "Abcd1234"
}
```

**Response:**

The information of logged in account

```json
{
    "name": "Phan Dung Tri",
    "email": "phandungtri@email.com",
    "password": "Iamhashed",
    "address": "HCMC",
    "phone": "0326123456",
    "id": 1
}
```

**Cookies:**

| **name** | **description**           |
| -------- | ------------------------- |
| `UserId` | `id` of logged in account |

---

# Combo

## `GET /api/combo`

> Get all available combos

**Queries:**

| **Key**    | **Type** | **Description**                |
| ---------- | -------- | ------------------------------ |
| `contain`  | `number` | Search combos that contains id |                                           |

**Response:**

```json
[
    {
        "name": "Test combo",
        "discount": 15,
        "price": 17.85,
        "originPrice": 21,
        "details": [
            {
                "product": {
                    "name": "abcd d",
                    "amount": 0,
                    "price": 10,
                    "description": "qwdqwhioh",
                    "category": {
                        "name": "CPU",
                        "id": 1
                    },
                    "image": "acd",
                    "id": 1
                },
                "amount": 1
            },
            {
                "product": {
                    "name": "mndqwdm wd",
                    "amount": 0,
                    "price": 5,
                    "description": "qwdnqwoi qdwiodn",
                    "category": {
                        "name": "CPU",
                        "id": 1
                    },
                    "image": "wdqd",
                    "id": 2
                },
                "amount": 1
            },
            {
                "product": {
                    "name": "dqwd",
                    "amount": 0,
                    "price": 6,
                    "description": "wger",
                    "category": {
                        "name": "CPU",
                        "id": 1
                    },
                    "image": "qwg",
                    "id": 4
                },
                "amount": 1
            }
        ],
        "id": 1
    },
    {
        "name": "Test combo 2",
        "discount": 15,
        "price": 17.85,
        "originPrice": 21,
        "details": [
            {
                "product": {
                    "name": "abcd d",
                    "amount": 0,
                    "price": 10,
                    "description": "qwdqwhioh",
                    "category": {
                        "name": "CPU",
                        "id": 1
                    },
                    "image": "acd",
                    "id": 1
                },
                "amount": 1
            },
            {
                "product": {
                    "name": "mndqwdm wd",
                    "amount": 0,
                    "price": 5,
                    "description": "qwdnqwoi qdwiodn",
                    "category": {
                        "name": "CPU",
                        "id": 1
                    },
                    "image": "wdqd",
                    "id": 2
                },
                "amount": 1
            }
        ],
        "id": 1
    }
]
```

---

## `GET /api/combo/:id`

> Get information of combo by its `id`

**Response:**

```json
{
    "name": "Test combo",
    "discount": 15,
    "price": 17.85,
    "originPrice": 21,
    "details": [
        {
            "product": {
                "name": "abcd d",
                "amount": 0,
                "price": 10,
                "description": "qwdqwhioh",
                "category": {
                    "name": "CPU",
                    "id": 1
                },
                "image": "acd",
                "id": 1
            },
            "amount": 1
        },
        {
            "product": {
                "name": "mndqwdm wd",
                "amount": 0,
                "price": 5,
                "description": "qwdnqwoi qdwiodn",
                "category": {
                    "name": "CPU",
                    "id": 1
                },
                "image": "wdqd",
                "id": 2
            },
            "amount": 1
        },
        {
            "product": {
                "name": "dqwd",
                "amount": 0,
                "price": 6,
                "description": "wger",
                "category": {
                    "name": "CPU",
                    "id": 1
                },
                "image": "qwg",
                "id": 4
            },
            "amount": 1
        }
    ],
    "id": 1
}
```

---

## `POST /api/combo`

> Create a new product combo

**Body:**

```json
{
    "name": "Test combo",
    "discount": 15,
    "details": [
        {
            "product": {
                "name": "abcd d",
                "amount": 0,
                "price": 10,
                "description": "qwdqwhioh",
                "category": {
                    "name": "CPU",
                    "id": 1
                },
                "image": "acd",
                "id": 1
            },
            "amount": 1
        },
        {
            "product": {
                "name": "mndqwdm wd",
                "amount": 0,
                "price": 5,
                "description": "qwdnqwoi qdwiodn",
                "category": {
                    "name": "CPU",
                    "id": 1
                },
                "image": "wdqd",
                "id": 2
            },
            "amount": 1
        },
        {
            "product": {
                "name": "dqwd",
                "amount": 0,
                "price": 6,
                "description": "wger",
                "category": {
                    "name": "CPU",
                    "id": 1
                },
                "image": "qwg",
                "id": 4
            },
            "amount": 1
        }
    ]
}
```

*Note: with `product` property, only its `id` property is required, the other properties can be omitted.*

---
