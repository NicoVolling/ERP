>**CurrentVersion:** BETA v0.92

### **What is ERP?**

ERP is a library that allows you to develop your own ERP application in a simple and developer-friendly way. Of course you don't necessarily need this library, but it takes a lot of work and ensures a consistent developer-friendliness. The potential for errors is reduced by escalation of errors until they reach the client. In addition, all API functions are formulated as C# methods, so that the developer can simply use them on the server and client side without running into an error.
The business library allows you to manage objects entirely from the API. This means that the storage is automatic and some functions are implemented by default (GetList, GetObjects, GetExistence, GetData, Create, Change, Delete). Of course, this does not exclude additional functions from being implemented. For example, it is possible to implement an additional "Login" function for the managed "User".
Of course, there are also security features for business management: Each standard function requires a Guid (SECURITY_CODE). To query whether the security code is valid and the user is authorized, the corresponding method (e.g. OnGetList) can be overwritten.
To make it even easier for the developer, all API functions are automatically output in table form via a GetRequest. So you can always investigate and test all API functions categorized in the web browser.
In order to ensure a seamless transition from backend to frontend, there are also parsers and own projects for data binding in WindowsForms.

### **How are the data transmitted?**

When sending a request to the server, a DataInput is sent along (serialization with JSON). This contains a command (namespace, class and method) and a collection of arguments (e.g. SECURITY_CODE).
On the server side, the DataInput is then interpreted and forwarded to the corresponding class (CommandCollection). There the arguments are checked and deserialized (JSON). If everything is correct, the corresponding method is executed and the supplied parameters are passed. So we are now in the code on the server side. This may be business code or code that has been developed itself (e.g. Create is managed vs Login is not implemented by default).
If an exception is triggered at any time, it is escalated and passed to the client and thrown as an exception.

### **How are the managed data stored**

All objects of the same type are stored in a list and serialized (JSON). For the time being, the serialized string is stored on the server in a file as it is. Later it should be encrypted. At the moment this would be rather hindering from the development point of view.
