# Status

## Get the Developer on call

We need a way for users of our API to make a request to see who the current on call developer is, what their email and phone is, and when it was last checked.


**REQUEST**
```
GET http://localhost:1337/status/on-call-developer
```

**RESPONSE**

```
200 Ok
Content-Type: application/json

{
    "currentDeveloper": "Bob Smith",
    "phone": "555-1212",
    "email": "bob@aol.com",
    "lastChecked": "ISO 8601 String of a Date"
}

```