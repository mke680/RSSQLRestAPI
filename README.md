# RSSQLRestAPI
3rd Party C# Rest API (Proof of Concept) Awaiting approval for further development
![image](https://user-images.githubusercontent.com/55390802/120596565-4314d580-c487-11eb-81bd-51c2b0eb902e.png)

This is an OpenAPI compliant REST API build to access the RS-SQL Record Management Database

- Read access control is restricted via T-SQL
- Write access control is build into the 3rd party C# API
- Main function is to support automation and proactive maintanence
- Secondary Functions
  - API is used to facilate SSO
![image](https://user-images.githubusercontent.com/55390802/120648998-9d318d00-c4bf-11eb-9fe3-e64d9edd86dc.png)
    - OEM application website doesn't support single sign on
      - Rest API option is available but not commerically viable. Subscription based and support based model $$$$$
      - Only C# dotNet API left as viable option
    - Vuejs MSOL can be used for SSO and is used to authenticate
    - RSSQLRestAPI will use service account to return application usercode when given a UPN/Email
      - If Username is not found JIT Provisioning will create a fresh user assigned to UPN/Email (WIP)
    - When usercode is returned successfully password will be reset and stored in token for login session (WIP) 
