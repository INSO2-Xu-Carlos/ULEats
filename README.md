# ULEats

ULEats es una aplicación de delivery, con frontend en Vue 3 y backend en C# (.NET), desplegada en Render.

## 🛠️ Tecnologías

- **Frontend**: Vue 3, Composition API, Vite
- **Backend**: C# (.NET 8), ASP.NET Core
- **Base de datos**: Neon (PostgreSQL)
- **Infraestructura**: Render (Web Service + Static Site)
- **Estructura**: Monorepo con separación clara de frontend/backend

## 📁 Estructura del proyecto
```
ULEats
├── backend
│   ├── backend
│   │   ├── AppDataConnection.cs
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── backend.csproj
│   │   ├── backend.csproj.user
│   │   ├── backend.http
│   │   ├── backend.sln
│   │   ├── Controllers
│   │   │   ├── ClientController.cs
│   │   │   ├── CustomerController.cs
│   │   │   ├── DeliveryController.cs
│   │   │   ├── OrderController.cs
│   │   │   ├── OrderItemController.cs
│   │   │   ├── OrderTrackingController.cs
│   │   │   ├── PaymentController.cs
│   │   │   ├── ProductController.cs
│   │   │   └── RestaurantController.cs
│   │   ├── Core
│   │   │   ├── ClientService.cs
│   │   │   ├── CustomerService.cs
│   │   │   ├── DeliveryService.cs
│   │   │   ├── OrderItemService.cs
│   │   │   ├── OrderService.cs
│   │   │   ├── OrderTrackingService.cs
│   │   │   ├── PaymentService.cs
│   │   │   ├── ProductService.cs
│   │   │   └── RestaurantService.cs
│   │   ├── Dockerfile
│   │   ├── Model
│   │   │   ├── CustomerCreateDTO.cs
│   │   │   ├── Customer.cs
│   │   │   ├── DeliveryCreateDTO.cs
│   │   │   ├── Delivery.cs
│   │   │   ├── GenericItem.cs
│   │   │   ├── NeonAuthSchema.cs
│   │   │   ├── OrderCreateDTO.cs
│   │   │   ├── Order.cs
│   │   │   ├── OrderItemCreateDTO.cs
│   │   │   ├── OrderItem.cs
│   │   │   ├── OrderitemWithProductNameDto.cs
│   │   │   ├── OrderStatusDeliveryUpdateDTO.cs
│   │   │   ├── OrderTrackingCreateDTO.cs
│   │   │   ├── OrderTracking.cs
│   │   │   ├── OrderTrackingDTO.cs
│   │   │   ├── PaymentCreateDTO.cs
│   │   │   ├── PaymentCreateTDO.cs
│   │   │   ├── Payment.cs
│   │   │   ├── ProductCreateDTO.cs
│   │   │   ├── Product.cs
│   │   │   ├── Restaurant.cs
│   │   │   ├── RestaurantDTO.cs
│   │   │   ├── ResturantCreateDTO.cs
│   │   │   ├── UlEatsDb.cs
│   │   │   └── User.cs
│   │   ├── Program.cs
│   │   ├── Properties
│   │   │   └── launchSettings.json
│   │   └── WeatherForecast.cs
│   ├── packages-microsoft-prod.deb
│   ├── TestULEats
│   │   ├── TestULEats.csproj
│   │   └── UnitTest1.cs
│   └── ULEats-backend
│       ├── appsettings.Development.json
│       ├── appsettings.json
│       ├── Controllers
│       │   └── WeatherForecastController.cs
│       ├── Program.cs
│       ├── Properties
│       │   └── launchSettings.json
│       ├── ULEats-backend.csproj
│       ├── ULEats-backend.csproj.user
│       ├── ULEats-backend.http
│       ├── ULEats-backend.sln
│       └── WeatherForecast.cs
├── docker-compose.yml
├── Dockerfile
├── frontend
│   └── ULEats
│       ├── dist
│       │   ├── assets
│       │   │   ├── index-Bk6uZx-1.js
│       │   │   ├── index-CvIAjgwG.css
│       │   │   ├── materialdesignicons-webfont-B7mPwVP_.ttf
│       │   │   ├── materialdesignicons-webfont-CSr8KVlo.eot
│       │   │   ├── materialdesignicons-webfont-Dp5v-WZN.woff2
│       │   │   └── materialdesignicons-webfont-PXm3-2wK.woff
│       │   ├── favicon.ico
│       │   └── index.html
│       ├── env.d.ts
│       ├── eslint.config.js
│       ├── index.html
│       ├── package.json
│       ├── package-lock.json
│       ├── public
│       │   └── favicon.ico
│       ├── README.md
│       ├── src
│       │   ├── App.vue
│       │   ├── assets
│       │   │   ├── logo.png
│       │   │   └── logo.svg
│       │   ├── auto-imports.d.ts
│       │   ├── components
│       │   │   ├── AcceptedOrders.vue
│       │   │   ├── AppFooter.vue
│       │   │   ├── CartDrawer.vue
│       │   │   ├── LoginButton.vue
│       │   │   ├── LogoutButton.vue
│       │   │   ├── NewAddress.vue
│       │   │   ├── Payment.vue
│       │   │   ├── PendingOrders.vue
│       │   │   ├── ProductCartList.vue
│       │   │   ├── ProductList.vue
│       │   │   ├── README.md
│       │   │   ├── RegisterButton.vue
│       │   │   ├── RestaurantList.vue
│       │   │   └── UserOrders.vue
│       │   ├── components.d.ts
│       │   ├── layouts
│       │   │   ├── HomeLayout.vue
│       │   │   └── README.md
│       │   ├── main.ts
│       │   ├── pages
│       │   │   ├── ClientPage.vue
│       │   │   ├── DeliveryPage.vue
│       │   │   ├── HomePage.vue
│       │   │   ├── LoginPage.vue
│       │   │   ├── PaymentPage.vue
│       │   │   ├── ProductForm.vue
│       │   │   ├── README.md
│       │   │   ├── RegisterCustomerPage.vue
│       │   │   ├── RegisterDeliveryPage.vue
│       │   │   ├── RegisterPage.vue
│       │   │   ├── RegisterRestaurantPage.vue
│       │   │   └── RestaurantPage.vue
│       │   ├── plugins
│       │   │   ├── index.ts
│       │   │   ├── README.md
│       │   │   └── vuetify.ts
│       │   ├── router
│       │   │   └── index.ts
│       │   ├── stores
│       │   │   ├── app.ts
│       │   │   ├── index.ts
│       │   │   └── README.md
│       │   ├── styles
│       │   │   ├── README.md
│       │   │   └── settings.scss
│       │   └── typed-router.d.ts
│       ├── tsconfig.app.json
│       ├── tsconfig.json
│       ├── tsconfig.node.json
│       └── vite.config.mts
├── Model
│   ├── Customer.cs
│   ├── Delivery.cs
│   ├── NeonAuthSchema.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── OrderTracking.cs
│   ├── Payment.cs
│   ├── Product.cs
│   ├── Restaurant.cs
│   ├── UlEatsDb.cs
│   └── User.cs
└── README.md
```
## 🚀 Instalación local

### Prerrequisitos

- Node.js v22+ (para frontend)
- .NET SDK 8 (para backend)

### Configuración inicial

git clone https://github.com/INSO2-Xu-Carlos/ULEats.git
cd ULEats

Frontend
cd frontend/ULEats
npm install
npm run dev

Backend
Desde Visual Studio 2022 se abre el archivo backend.sln en la ruta /backend/backend y ejecutamos en IIS Express

## Despliegue
El proyecto está configurado para desplegarse en Render.
URL de la Pagina web 
https://uleats-1.onrender.com

### Notas 
Asegurarse en todo momento que el backend este "despierto" 
https://uleats-8xnb.onrender.com 
