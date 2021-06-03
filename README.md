# RSSQLRestAPI
DotNet Core Rest API (Proof of Concept) Awaiting approval for further development

This is an OpenAPI compliant REST API build to access the RS-SQL Record Management Database

- Read access control is restricted via T-SQL
- Write access control is build into the 3rd party C# API
- Main function is to support automation and proactive maintanence
- Secondary Functions
  - API is used to facilate SSO
![image](https://user-images.githubusercontent.com/55390802/120656552-0f59a000-c4c7-11eb-8665-e2892487785b.png)
    - OEM application website doesn't support single sign on
      - Rest API option is available but not commerically viable. Subscription based and support based model $$$$$
      - Only C# dotNet API left as viable option
    - MSAL.js or ADAL.js or other can be used for SSO 
    - RSSQLRestAPI will use service account to return RSSQL usercode when given an SSO ID
    ![image](https://user-images.githubusercontent.com/55390802/120650453-285f5280-c4c1-11eb-9e79-be5756f8a822.png)
      - If Username is not found JIT Provisioning will create a fresh user assigned to UPN/Email (WIP)
    - When usercode is returned successfully password will be reset and stored in token for login session (WIP) 
