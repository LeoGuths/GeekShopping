*Create C:\Tools\LocalNuget;

*Create databases (geek_shopping_product_api, geek_shopping_identity_server, geek_shopping_cart_api, geek_shopping_coupon_api, geek_shopping_order_api, geek_shopping_email)
*Run 'dotnet tool install --global dotnet-ef';
*Run 'dotnet ef migrations add AddAndSeedProductDataTableOnDB' to create ProductAPI migration;
*Run 'dotnet ef migrations add AddDefaultSecurityTablesOnDb' to create IdentityServer migration;
*Run 'dotnet ef migrations add AddCartDataTableOnDB' to create CartAPI migration;
*Run 'dotnet ef migrations add AddCouponDataTablesOnDB' to create CouponAPI migration;
*Run 'dotnet ef migrations add AddOrderDataTablesOnDB' to create OrderAPI migration;
*Run 'dotnet ef migrations add AddEmailDatatablesOnDB' to create Email migration;
*Run 'dotnet ef database update' on ProductAPI, IdentityServer, CartAPI, CouponAPI, OrderAPI and Email projects;

*docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management