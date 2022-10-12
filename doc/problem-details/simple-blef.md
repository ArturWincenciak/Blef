# Simple Blef Exception

`title` (duplicated in `details`) gives message what was wrong

Example:

```
HTTP/1.1 400 Bad Request
Connection: close
Content-Type: application/problem+json; charset=utf-8
Date: Wed, 12 Oct 2022 06:50:58 GMT
Server: Kestrel
Transfer-Encoding: chunked
Trace-Id: 0HMLC3L0UNJT5:00000002
Activity-Id: 00-abfaad478912cb8e92e63688787180c1-c0528396bee3011e-00

{
  "type": "https://github.com/ArturWincenciak/Blef/blob/main/doc/problem-details/simple-blef.md",
  "title": "Cannot join game that is already started",
  "status": 400,
  "detail": "Cannot join game that is already started",
  "traceId": "0HMLC3L0UNJT5:00000002",
  "activityId": "00-abfaad478912cb8e92e63688787180c1-c0528396bee3011e-00"
}
```
