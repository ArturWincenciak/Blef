# Internal Server Error

---

TBD

---

Implements the [RFC 7807](https://www.rfc-editor.org/rfc/rfc7807.html) standard called 'Problem Details for HTTP APIs'.

---

## Example response

```
HTTP/1.1 500 Internal Server Error
Connection: close
Content-Type: application/problem+json; charset=utf-8
Date: Sun, 04 Sep 2022 10:22:47 GMT
Server: Kestrel
Transfer-Encoding: chunked
Trace-Id: 0HMKEBHG474CG:00000002
Activity-Id: 00-0353bf976d50d1372d474cae96972c29-5b4d00cbd6abee48-00

{
  "type": "https://github.com/ArturWincenciak/blef/doc/problem-details/internal-server-error.md",
  "title": "Internal server error",
  "status": 500,
  "detail": "Unexpected error occurred",
  "traceId": "0HMKEBHG474CG:00000002",
  "activityId": "00-0353bf976d50d1372d474cae96972c29-5b4d00cbd6abee48-00"
}
```