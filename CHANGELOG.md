# Changelog

All notable changes to this project will be documented in this file.



## [1.1.0] - 2026-04-07
### Added
- **Docker Support:** Introduced `Dockerfile` with a multi-stage `.NET 10` setup for optimized container deployments.
- **Docker Compose:** Added `docker-compose.yml` mapped to `ASPNETCORE_ENVIRONMENT=Development` ensuring developers can access Swagger UI immediately upon container launch.
- Expanded `README.md` to include execution instructions for Docker, detailing the exact commands to spin up the service.

### Changed
- **Core Library Swap:** Fully migrated from the obsolete `System.Net.Mail` framework to **`MailKit`** and **`MimeKit`** for modern, secure email compliance.
- Upgraded SMTP authentication flow inside `EmailService.cs` incorporating smart `SecureSocketOptions` parsing (e.g. `SslOnConnect` for Port 465, `StartTls` for Port 587).
- Updated internal system architecture documentation on `README.md` to properly uncover the asynchronous background processing engine (`IEmailQueue`, `EmailSenderWorker`, `EmailQueueItem`).
- Patched incomplete JSON schemas inside the `README.md` ensuring the `/api/email/send-code` path displays all required model attributes (`subject`, `body`, `isHtml`).

## [1.0.0] - 2026-04-07
### Added
- Initial API endpoints for email and code email sending.
- SMTP-based email delivery service.
- Background queue worker with retry logic.
- Request validation, rate limiting, and Swagger/OpenAPI setup.
- CI pipeline for restore, build, and test.
