*Create C:\Tools\LocalNuget;

*Create databases (geek_shopping_product_api, geek_shopping_identity_server, geek_shopping_cart_api, geek_shopping_coupon_api)
*Run 'dotnet tool install --global dotnet-ef';
*Run 'dotnet ef migrations add AddAndSeedProductDataTableOnDB' to create ProductAPI migration;
*Run 'dotnet ef migrations add AddDefaultSecurityTablesOnDb' to create IdentityServer migration;
*Run 'dotnet ef migrations add AddCartDataTableOnDB' to create CartAPI migration;
*Run 'dotnet ef migrations add AddCouponDataTablesOnDB' to create CouponAPI migration;
*Run 'dotnet ef database update' on ProductAPI, IdentityServer, CartAPI and CouponAPI projects;