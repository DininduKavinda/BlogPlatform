namespace TaskManagementApp.Utilities
{
    public static class EmailTemplates
    {
        public static string GetPasswordResetTemplate(string resetLink)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .button {{ display: inline-block; padding: 12px 24px; background-color: #007bff; color: white; text-decoration: none; border-radius: 4px; }}
                        .footer {{ margin-top: 20px; padding-top: 20px; border-top: 1px solid #eee; color: #666; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Password Reset Request</h2>
                        <p>You requested to reset your password. Click the button below to proceed:</p>
                        <p><a href='{resetLink}' class='button'>Reset Password</a></p>
                        <p>This link will expire in 1 hour for security reasons.</p>
                        <p>If you didn't request a password reset, please ignore this email.</p>
                        <div class='footer'>
                            <p>Best regards,<br>Your Application Team</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        public static string GetWelcomeTemplate(string email)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .welcome {{ color: #28a745; }}
                        .footer {{ margin-top: 20px; padding-top: 20px; border-top: 1px solid #eee; color: #666; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2 class='welcome'>Welcome, {email}!</h2>
                        <p>Thank you for joining our application. We're excited to have you on board!</p>
                        <p>Your account has been successfully created and you can now access all features.</p>
                        <div class='footer'>
                            <p>Best regards,<br>Your Application Team</p>
                        </div>
                    </div>
                </body>
                </html>";
        }
    }
}