# 📧 E-Mail Senders API (BETA)

A RESTful Web API built with **C# ASP.NET Core** that allows users to send emails using their own SMTP credentials. All SMTP configuration and — when applicable — the email code/content are provided **per request** by the caller, so no server-side email credentials need to be stored.

---

## 🚀 Features

- Send emails using **caller-supplied SMTP credentials** (no server-side config required)
- Support for **plain-text and HTML** email bodies
- **Verification / OTP code sending** — caller provides the code to be sent
- Input validation for SMTP fields and email addresses
- Swagger / OpenAPI documentation

---

## 🛠️ Tech Stack

| Layer         | Technology                  |
|---------------|-----------------------------|
| Language      | C# (.NET 10)                |
| Framework     | ASP.NET Core Web API        |
| Email Library | MailKit / System.Net.Mail   |
| Documentation | Swagger (Swashbuckle)       |

---

## 📁 Project Structure

```
e-mailsenders/
├── Controllers/
│   └── EmailController.cs       # API endpoints
├── Models/
│   ├── SendEmailRequest.cs      # Standard email request model
│   └── SendCodeRequest.cs       # Code/OTP email request model
├── Services/
│   └── EmailService.cs          # SMTP send logic
├── appsettings.json
├── Program.cs
└── README.MD
```

---

## ⚙️ Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A valid SMTP server (e.g. Gmail, Outlook, custom SMTP)

---

## 🏃 Running the Application

```bash
# Restore dependencies
dotnet restore

# Run the API
dotnet run
```

The API will be available at: `https://localhost:5001`  
Swagger UI: `https://localhost:5001/swagger`

---

## 📬 API Endpoints

### 1. Send a Standard Email

**`POST /api/email/send`**

Sends an email using the SMTP credentials provided in the request body.

#### Request Body

```json
{
  "smtp": {
    "host": "smtp.gmail.com",
    "port": 587,
    "username": "your-email@gmail.com",
    "password": "your-app-password",
    "enableSsl": true
  },
  "from": "your-email@gmail.com",
  "to": "recipient@example.com",
  "subject": "Hello from E-Mail Senders API",
  "body": "This is the email body.",
  "isHtml": false
}
```

#### SMTP Fields

| Field       | Type    | Required | Description                                      |
|-------------|---------|----------|--------------------------------------------------|
| `host`      | string  | ✅       | SMTP server hostname (e.g. `smtp.gmail.com`)     |
| `port`      | integer | ✅       | SMTP port (typically `587` for TLS, `465` for SSL) |
| `username`  | string  | ✅       | SMTP login username / email address               |
| `password`  | string  | ✅       | SMTP login password or app-specific password      |
| `enableSsl` | boolean | ✅       | Whether to use SSL/TLS (`true` recommended)       |

#### Email Fields

| Field     | Type    | Required | Description                              |
|-----------|---------|----------|------------------------------------------|
| `from`    | string  | ✅       | Sender email address                     |
| `to`      | string  | ✅       | Recipient email address                  |
| `subject` | string  | ✅       | Subject line of the email                |
| `body`    | string  | ✅       | Body content of the email                |
| `isHtml`  | boolean | ❌       | Set `true` to send HTML body (default: `false`) |

#### Responses

| Status | Description                          |
|--------|--------------------------------------|
| `200`  | Email sent successfully              |
| `400`  | Validation error (missing/invalid fields) |
| `500`  | SMTP connection or authentication failed |

---

### 2. Send a Code / OTP Email

**`POST /api/email/send-code`**

Sends a verification code or OTP to the specified email address using caller-supplied SMTP credentials and a caller-supplied code.

#### Request Body

```json
{
  "smtp": {
    "host": "smtp.gmail.com",
    "port": 587,
    "username": "your-email@gmail.com",
    "password": "your-app-password",
    "enableSsl": true
  },
  "from": "your-email@gmail.com",
  "to": "recipient@example.com",
  "code": "483920"
}
```

#### Code Fields

| Field  | Type   | Required | Description                                           |
|--------|--------|----------|-------------------------------------------------------|
| `to`   | string | ✅       | Recipient email address                               |
| `from` | string | ✅       | Sender email address                                  |
| `code` | string | ✅       | The verification code / OTP to include in the email   |

> The API wraps the provided `code` in a pre-built email template and sends it to the recipient.

#### Responses

| Status | Description                                 |
|--------|---------------------------------------------|
| `200`  | Code email sent successfully                |
| `400`  | Validation error (missing/invalid fields)   |
| `500`  | SMTP connection or authentication failed    |

---

## 📨 Example — Gmail SMTP Setup

To use Gmail as your SMTP provider:

1. Enable **2-Step Verification** on your Google account.
2. Generate an **App Password**: [myaccount.google.com/apppasswords](https://myaccount.google.com/apppasswords)
3. Use the following SMTP settings in your request:

```json
"smtp": {
  "host": "smtp.gmail.com",
  "port": 587,
  "username": "your-email@gmail.com",
  "password": "your-16-char-app-password",
  "enableSsl": true
}
```

---

## � Security Notes

> ⚠️ **SMTP credentials are passed in the request body and are NOT stored or logged by the API.**

- Always use HTTPS in production to protect credentials in transit.
- Prefer **App Passwords** over your main account password when using Gmail or Outlook.
- Never expose this API publicly without additional rate limiting or authentication.

---

## 🧪 Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage report
dotnet test --collect:"XPlat Code Coverage"
```

---

## 📝 License

This project is licensed under the [MIT License](LICENSE).

---

## 🤝 Contributing

Contributions are welcome! Please open an issue first to discuss what you'd like to change.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📧 Contact

For questions or support, please open an [issue](https://github.com/denizpekova/e-mailsender/issues).
