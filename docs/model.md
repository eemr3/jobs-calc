## Table User

- name
- emal
- password
- avatar_url
- created_at

## Table Planning

- monthly_budget
- days_per_week
- hours_per_day
- vacation_per_year
- value_hour

## Table Jobs

- name
- daily_hours
- total_hours
- created_at

JobsCalc/
├── JobsCalc.sln  
├── src/
│ └── JobsCalc/  
│ ├── Http/
│ │ ├── Controllers/
│ │ └── Dtos/
│ ├── Domain/
│ │ └── Entities/
│ ├── Application/
│ │ └── Services/
│ ├── Infra/
│ │ └── Database/
│ │ ├── EntityFramework/
│ │ │ └──Migrations/
│ │ └──Repositories/
│ ├── Program.cs
│ ├── FinanceManager.csproj
│ └── ...
├── docs/  
│ └── model.md
├── .gitignore  
├── README.md  
├── .env.example
├── .env
├── docker-compose.yml
└── Dockerfile
