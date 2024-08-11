using HAMDA.Core.Extentions;
using HAMDA.Data;
using HAMDA.Models.EntityModels.Enum;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HAMDA.Service.Service
{
    public class Costumer : ICostumer
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IAttachmentService _attachmentService;
        public Costumer(ApplicationDbContext context, IEmailSender emailSender, IAttachmentService attachmentService)
        {
            _context = context;
            _emailSender = emailSender;
            _attachmentService = attachmentService;
        }

        public async Task<(bool response,bool isValid)> AddCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model)
        {
            var EntityModel = new HAMDA.Models.EntityModels.Costumer.CostumerRegister
            {
                Country = Model.Country,
                Email = Model.Email,
                Phone = Model.Phone,
                Username = Model.Username,
                Status = (int)Status.Pending,
            };

            var isValid = (await _context.Costumers.CountAsync(x=>x.Email == Model.Email && x.Status == (int)Status.Pending)) == 0;

            if (isValid) {
                var returnedResponse = await _context.Costumers.AddAsync(EntityModel);

                if (await _context.SaveChangesAsync() > 0)
                {
                    Model.CurrentLanguage = Model.CurrentLanguage.IsNotNullOrEmpty() && (Model.CurrentLanguage == "ar" || Model.CurrentLanguage == "en") ? Model.CurrentLanguage : "en";
                    var htmlMessage = GenerateEmailMessage(Model.Username,Model.CurrentLanguage);

                    await _emailSender.SendEmailAsync(Model.Email, htmlMessage.headerMessage, htmlMessage.htmlMessage.ToString());
                    return (true,isValid);
                }
            }
            return (false, isValid);
        }


        private (string htmlMessage, string headerMessage) GenerateEmailMessage(string recipientName, string currentLang)
        {
            var htmlMessage = new StringBuilder();
            var headerMessage = "";

            if (currentLang == "ar")
            {
                headerMessage = "تم استلام طلبك للمشاركة في ماستر كلاس حمدة للمكياج";

                htmlMessage.AppendLine($"عزيزي/عزيزتي {recipientName}،<br><br>");
                htmlMessage.AppendLine("شكرًا لك على تقديم طلبك للمشاركة في ماستر كلاس حمدة للمكياج! نحن سعداء بأنك اخترت الانضمام إلينا في هذا الحدث الفريد، حيث ستشارك حمدة أسرارها وتقنياتها وراء بعض من أشهر إطلالاتها في عالم المكياج.<br><br>");
                htmlMessage.AppendLine("تفاصيل الحدث:<br><br>");
                htmlMessage.AppendLine("التاريخ: 26 أغسطس 2024<br>");
                htmlMessage.AppendLine("الوقت: من الساعة 5:00 مساءً إلى الساعة 10:00 مساءً<br>");
                htmlMessage.AppendLine("الموقع: فندق ذا لانا - مجموعة دورشيستر<br><br>");
                htmlMessage.AppendLine("تعليمات الدفع:<br><br>");
                htmlMessage.AppendLine("يرجى اتباع الخطوات أدناه لإتمام عملية الدفع:<br><br>");
                htmlMessage.AppendLine("اسم البنك: مصرف أبوظبي الإسلامي<br>");
                htmlMessage.AppendLine("اسم صاحب الحساب: حمدة عبدالله حسن العلي<br>");
                htmlMessage.AppendLine("رقم الحساب: 28272955<br>");
                htmlMessage.AppendLine("رقم الآيبان: AE160500000000028272955<br>");
                htmlMessage.AppendLine("السويفت كود: ABDIAEADXXX<br>");
                htmlMessage.AppendLine("العملة: درهم إماراتي<br>");
                htmlMessage.AppendLine("المبلغ: 2500 درهم<br><br>");
                htmlMessage.AppendLine("بعد إتمام عملية الدفع، يرجى التقاط لقطة شاشة أو صورة لتأكيد الدفع. يمكنك إرسال تأكيد الدفع من خلال النقر على زر الواتساب المرفق أدناه أو التواصل معنا مباشرة عبر واتساب على الرقم +971566517607.<br><br>");
                htmlMessage.AppendLine("معلومات هامة:<br><br>");
                htmlMessage.AppendLine("• يرجى الوصول قبل 15 دقيقة على الأقل لضمان عملية تسجيل سلسة.<br>");
                htmlMessage.AppendLine("• سيتم توفير جميع الأدوات والمواد اللازمة للماستر كلاس.<br><br>");
                htmlMessage.AppendLine("إذا كان لديك أي استفسارات أو تحتاج إلى مزيد من المساعدة، لا تتردد في التواصل معنا عبر واتساب أو الرد على هذا البريد الإلكتروني.<br><br>");
                htmlMessage.AppendLine("نتطلع لرؤيتك هناك!<br><br>");
                htmlMessage.AppendLine("مع أطيب التحيات،<br>");
                htmlMessage.AppendLine("فريق حمدة للمكياج<br>");
                htmlMessage.AppendLine("<a href=\"https://hamdamakeup.com\">https://hamdamakeup.com</a> | +971566517607<br>");
            }
            else // Default to English if not Arabic
            {
                headerMessage = "Your Request for Hamda Makeup Masterclass Participation Has Been Received!";

                htmlMessage.AppendLine($"Dear {recipientName},<br><br>");
                htmlMessage.AppendLine("Thank you for submitting your request to participate in the Hamda Makeup Masterclass! We are excited that you’ve chosen to join us for this unique event, where Hamda will share her secrets and techniques behind some of her most iconic makeup looks.<br><br>");
                htmlMessage.AppendLine("Event Details:<br><br>");
                htmlMessage.AppendLine("Date: 26th August 2024<br>");
                htmlMessage.AppendLine("Time: 5:00 PM - 10:00 PM<br>");
                htmlMessage.AppendLine("Location: The Lana Hotel - Dorchester Collection<br><br>");
                htmlMessage.AppendLine("Payment Instructions:<br><br>");
                htmlMessage.AppendLine("Please follow the steps below to complete your payment:<br><br>");
                htmlMessage.AppendLine("Bank Name: ADIB<br>");
                htmlMessage.AppendLine("Account Holder Name: Hamda Abdalla Hassan Al Ali<br>");
                htmlMessage.AppendLine("Account Number: 28272955<br>");
                htmlMessage.AppendLine("IBAN: AE160500000000028272955<br>");
                htmlMessage.AppendLine("SWIFT: ABDIAEADXXX<br>");
                htmlMessage.AppendLine("Currency: AED<br>");
                htmlMessage.AppendLine("Amount: 2500 AED<br><br>");
                htmlMessage.AppendLine("After completing the payment, please take a screenshot or a photo of the payment confirmation. You can send the payment confirmation by clicking on the WhatsApp button provided below or contact us directly via WhatsApp at +971566517607.<br><br>");
                htmlMessage.AppendLine("Important Information:<br><br>");
                htmlMessage.AppendLine("• Please arrive at least 15 minutes early to ensure a smooth check-in process.<br>");
                htmlMessage.AppendLine("• All materials and tools needed for the masterclass will be provided.<br><br>");
                htmlMessage.AppendLine("If you have any questions or need further assistance, feel free to reach out via WhatsApp or reply to this email.<br><br>");
                htmlMessage.AppendLine("We look forward to seeing you there!<br><br>");
                htmlMessage.AppendLine("Best regards,<br>");
                htmlMessage.AppendLine("The Hamda Makeup Team<br>");
                htmlMessage.AppendLine("<a href=\"https://hamdamakeup.com\">https://hamdamakeup.com</a> | +971566517607<br>");
            }

            return (htmlMessage.ToString(),headerMessage);
        }


        public async Task<int> GetActiveCostumersCount()
        {
            var count = await _context.Costumers.CountAsync(x=>x.Status == 1);
            var numberOfSeats = (await _context.Configuration.FirstOrDefaultAsync(x=>x.Id==1)).NumberOfSeats;

            var avilableSeats = numberOfSeats - count;


            return avilableSeats;
        }

        public async Task<bool> UpdateActiveCostumersToOld()
        {
            var allActive = _context.Costumers.Where(x=>x.Status == 1);

            foreach (var item in allActive)
            {
                item.Status = 4;
            }
            _context.Costumers.UpdateRange(allActive);

            var IsUpdated = await _context.SaveChangesAsync() > 0;

            return IsUpdated;
        }
    }
}
