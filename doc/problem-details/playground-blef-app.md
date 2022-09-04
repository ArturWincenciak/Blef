# Playground Blef App

---

Example error used for the purpose of manually testing the error handling implementation and to demonstrate sample usage.

---

## Example responses

### Simple without validation error exrension 

```
HTTP/1.1 400 Bad Request
Connection: close
Content-Type: application/problem+json; charset=utf-8
Date: Sun, 04 Sep 2022 10:10:23 GMT
Server: Kestrel
Transfer-Encoding: chunked
Trace-Id: 0HMKEBHG474CD:00000002
Activity-Id: 00-0a28a43a67218f1a528307b102bdccf2-b12b6d86d42574d1-00

{
  "type": "https://github.com/ArturWincenciak/blef/doc/problem-details/playground-blef-app.md",
  "title": "Playground error",
  "status": 400,
  "detail": "Example Blef application error",
  "instance": "/Blef",
  "traceId": "0HMKEBHG474CD:00000002",
  "activityId": "00-0a28a43a67218f1a528307b102bdccf2-b12b6d86d42574d1-00"
}
```

### With validation error exrension 

```
HTTP/1.1 400 Bad Request
Connection: close
Content-Type: application/problem+json; charset=utf-8
Date: Sun, 04 Sep 2022 10:14:20 GMT
Server: Kestrel
Transfer-Encoding: chunked
Trace-Id: 0HMKEBHG474CE:00000002
Activity-Id: 00-b1b75ee259f77a5ca49f30436cc04805-a7cf0b6642718a37-00

{
  "type": "https://github.com/ArturWincenciak/blef/doc/problem-details/playground-blef-app.md",
  "title": "Playground error",
  "status": 400,
  "detail": "Example Blef application error",
  "instance": "/Blef",
  "traceId": "0HMKEBHG474CE:00000002",
  "activityId": "00-b1b75ee259f77a5ca49f30436cc04805-a7cf0b6642718a37-00",
  "errors": {
    "prop-1": [
      "contains-number",
      "is-not-unique"
    ],
    "prop-2": [
      "must-be-scalar"
    ]
  }
}
```