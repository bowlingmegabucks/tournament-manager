
# Tournament API Endpoints

## Get Tournament by ID

### Endpoint Overview

| Method | URL Path                | Description                                      |
|--------|------------------------|--------------------------------------------------|
| GET    | `/tournaments/{id}`    | Retrieves details of a specific tournament by ID. |

Returns the full details of a tournament specified by its unique identifier.

### Authentication & Authorization

- **Authentication**: None (endpoint is public/anonymous).
- **Authorization**: None required.
- **Rate Limiting**: May return 429 if rate limit is exceeded.

### Request Details

#### URL Parameters

| Name | Type   | Required | Description                        | Example                        |
|------|--------|----------|------------------------------------|--------------------------------|
| id   | string | Yes      | Unique identifier for the tournament. Must be a valid TournamentId. | `b1a2c3d4-e5f6-7890-abcd-ef1234567890` |

#### Request Headers

| Name          | Required | Description                |
|---------------|----------|----------------------------|
| Accept        | No       | Desired response format. Default: `application/json` |

#### Request Body

- None (GET request)

### Response Details

#### Success Response

- **Status Code**: `200 OK`
- **Content-Type**: `application/json`
- **Body**:

```json
{
    "ok": true,
    "data": {
        "id": "b1a2c3d4-e5f6-7890-abcd-ef1234567890",
        "name": "Spring Classic",
        "bowlingCenter": "Main Street Lanes",
        "games": 6,
        "completed": false,
        "startDate": "2025-04-01T00:00:00Z",
        "endDate": "2025-04-03T00:00:00Z",
        "entryFee": 75.00,
        "finalsRatio": 0.25,
        "cashRatio": 0.5,
        "superSweeperCashRatio": 0.1
    }
}
```

#### Error Responses

| Status | Content-Type             | Description                                 |
|--------|-------------------------|---------------------------------------------|
| 400    | application/problem+json| Invalid ID format or bad request            |
| 404    | application/problem+json| Tournament not found                        |
| 429    | application/problem+json| Rate limit exceeded                         |
| 500    | application/problem+json| Internal server error                       |

#### Example Error (400)

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Invalid Tournament ID",
    "status": 400,
    "detail": "The provided tournament ID is invalid.",
    "instance": "/tournaments/invalid-id"
}
```

#### Example Error (404)

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
    "title": "Not Found",
    "status": 404,
    "detail": "Tournament not found.",
    "instance": "/tournaments/b1a2c3d4-e5f6-7890-abcd-ef1234567890"
}
```

### Code Example

```bash
curl -X GET "https://api.example.com/tournaments/b1a2c3d4-e5f6-7890-abcd-ef1234567890" -H "Accept: application/json"
```

---

## Get All Tournaments (Paginated)

### Endpoint Overview

| Method | URL Path         | Description                                 |
|--------|-----------------|---------------------------------------------|
| GET    | `/tournaments/` | Retrieves all tournaments with pagination.  |

Returns a paginated list of all tournaments in the system.

### Authentication & Authorization

- **Authentication**: None (endpoint is public/anonymous).
- **Authorization**: None required.
- **Rate Limiting**: May return 429 if rate limit is exceeded.

### Request Details

#### Query Parameters

| Name     | Type   | Required | Default | Description                        | Example |
|----------|--------|----------|---------|------------------------------------|---------|
| page     | int    | No       | 1       | Page number (1-based)              | 2       |
| pageSize | int    | No       | 10      | Number of items per page           | 25      |

#### Request Headers

| Name          | Required | Description                |
|---------------|----------|----------------------------|
| Accept        | No       | Desired response format. Default: `application/json` |

#### Request Body

- None (GET request)

### Response Details

#### Success Response

- **Status Code**: `200 OK`
- **Content-Type**: `application/json`
- **Body**:

```json
{
    "ok": true,
    "data": [
        {
            "id": "b1a2c3d4-e5f6-7890-abcd-ef1234567890",
            "name": "Spring Classic",
            "bowlingCenter": "Main Street Lanes",
            "games": 6,
            "completed": false,
            "startDate": "2025-04-01T00:00:00Z",
            "endDate": "2025-04-03T00:00:00Z"
        }
        // ...more tournaments...
    ],
    "pagination": {
        "page": 1,
        "pageSize": 10,
        "totalPages": 5,
        "totalCount": 50
    }
}
```

#### Error Responses

| Status | Content-Type             | Description                                 |
|--------|-------------------------|---------------------------------------------|
| 400    | application/problem+json| Invalid query parameters                    |
| 429    | application/problem+json| Rate limit exceeded                         |
| 500    | application/problem+json| Internal server error                       |

#### Example Error (400)

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Bad Request",
    "status": 400,
    "detail": "Invalid page or pageSize parameter.",
    "instance": "/tournaments"
}
```

### Code Example

```bash
curl -X GET "https://api.example.com/tournaments?page=2&pageSize=25" -H "Accept: application/json"
```

---

## Notes & Considerations

- **Rate Limiting**: If rate limiting is enabled, 429 responses will be returned with appropriate headers (e.g., `Retry-After`).
- **Idempotency**: Both endpoints are idempotent (safe to call multiple times).
- **Performance**: Pagination is recommended for large result sets.
- **Business Rules**: Tournament IDs must be valid and exist in the system.
- **Related Endpoints**:
  - `POST /tournaments/` (create tournament)
  - `PUT /tournaments/{id}` (update tournament)
  - `DELETE /tournaments/{id}` (delete tournament)
