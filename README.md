# RSSQLRestAPI
3rd Party C# Rest API

This is an OpenAPI compliant REST API build to access the RS-SQL Record Management Database

- Read access control is restricted via T-SQL
- Write access control is build into the 3rd party C# API

- API is used to facilate SSO
![image](https://user-images.githubusercontent.com/55390802/120590375-c5000100-c47d-11eb-96ae-e033a410fdd4.png)
- 3rd Party application doesn't support single sign on
- Vuejs MSOL will be used for SSO and provide UPN/email for SSO authentication
- RSSQLRestAPI will use service account to return applcation usercode when given a UPN/Email
- If successful password will be reset and stored in token for login session
