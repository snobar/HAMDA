const translations = {
    en: {
        title1: "Hamda Makeup",
        title2: "Masterclass",
        title3: "HAS ARRIVED!",
        description:
            "For the first time, Hamda makeup artist invites you to experience her masterclass.",
        registerBtn: "Register Now",
        limitedSeats: "Limited seats.",
        locationDetails1: "Location: The Lana hotel - Dorchester Collection",
        locationDetails2: "26th August 2024",
        locationDetails3: " 5:00 pm to 10:00 pm",
        formHeader:
            "I will be sharing the secrets behind how I create some of my most iconic looks, including tips and techniques.",
        username: "Username",
        country: "Country",
        phone: "Phone",
        email: "Email",
        paymentsCount:"Payment Images Count",
        submit: "Continue",
        attachments: "Payment Images",
        attachmentsValidation: "You can upload up to 10 images.",
        firstName:"First Name",
        lastName:"Last Name",
        passowrd:"Passowrd",
        confirmPassword: "Confirm Password",
        addAdmin: "Add Admin User",
        pending:"Pending",
        deleted:"Deleted",
        approved: "Approved",
        next: "Next",
        previous: "السابق",
        noRecordsFound: "No records found.",
        details: "Requist Details",
        personalInformation: "Personal Information",
        delete:"Delete",
        approve:"Approve",
        download:"Download",
        noAttachmentsAvailable:"No attachments available.",
        backtoList: "Back to List",
        successMessage1: "Admin user added successfully!",
        successMessage2: "You have registered successfully!, we will reach you out soon.",
        somethingWentWrong:"Something Went Wrong!, Please Try Again."
    },
    ar: {
        title1: "حمده ميكاب آرتيست",
        title2: "ماستر كلاس",
        title3: "قد وصل!",
        description:
            "الآن ولأول مرة، تدعوكم خبيرة المكياج حمدا لتجربة ماستر كلاس المكياج الخاص بها.",
        registerBtn: "سجل الآن",
        limitedSeats: "المقاعد محدودة.",
        locationDetails1: "الموقع فندق لانا - مجموعة دورتشستر",
        locationDetails2: "26 أغسطس 2024",
        locationDetails3: "5:00 مساءً إلى 10:00 مساءً",
        formHeader:
            "سأشارككم الأسرار وراء كيفية إنشاء بعض من أكثر إطلالاتي شهرة، بما في ذلك النصائح والتقنيات.",
        username: "اسم المستخدم",
        country: "البلد",
        phone: "رقم الهاتف",
        email: "البريد الإلكتروني",
        paymentsCount: "عدد صور المدفوعات",
        submit: "متابعة",
        attachments: "صور المدفوعات",
        attachmentsValidation: "يمكن رفع لغاية 10 صور.",
        firstName: "الإسم الأول",
        lastName: "الإسم الأخير",
        passowrd: "كلمة السر",
        confirmPassword: "تأكيد كلمة السر",
        addAdmin: "إضافة مستخدم إداري",
        pending: "بإنتظار الموافقه",
        deleted: "محذوف",
        approved: "تمت الموافقه عليه",
        next: "التالي",
        previous: "السابق",
        noRecordsFound: "لا يوجد نتائج.",
        details: "تفاصيل الطلب",
        personalInformation: "المعلومات الشخصيه",
        delete: "مسح",
        approve: "قبول",
        download: "تحميل",
        noAttachmentsAvailable: "لا يوجد صور.",
        backtoList: "العودة الى القائمه",
        successMessage1: "تم اضافة مستخدم اداري جديد بنجاح!",
        successMessage2: "لقد قمت بالتسجيل بنجاح! سوف نتواصل معك قريبا.",
        somethingWentWrong: "حدث خطأ ما!، يرجى المحاولة مرة أخرى."
    },
};


const languageSelector = document.querySelector("#language-select");
const sliderContainer = document.querySelector(".slider");
const dotsContainer = document.querySelector(".dots-container");
const popup = document.getElementById("popup");
const openPopupBtn = document.getElementById("openPopup");
const closeBtn = document.querySelector(".close");
const form = document.getElementById("registrationForm");
let currentSlide = 0;
const slides = [
    {
        id: 1,
        title: "model1",
        img: "../Images/photo1.png",
    },
    {
        id: 2,
        title: "model2",
        img: "../Images/photo2.png",
    },
    {
        id: 3,
        title: "model3",
        img: "../Images/photo3.png",
    },
    {
        id: 4,
        title: "model4",
        img: "../Images/photo4.png",
    },
];
const language = localStorage.getItem("lang") || "en";

document.addEventListener("DOMContentLoaded", init);
languageSelector.addEventListener("click", onLanguageChange);
//form.addEventListener("submit", validateForm);
openPopupBtn.onclick = openPopup;
closeBtn.onclick = closePopup;
window.onclick = closePopupOnClickOutside;

function init() {
    setLanguage(language);
    if (sliderContainer) {
        setInterval(autoSlide, 5000);
        renderSlides();
        renderDots();
        updateSlider();
    }

}

function onLanguageChange() {
    let selectedLanguage = localStorage.getItem("lang");
    if (selectedLanguage == "en" || !selectedLanguage) {
        selectedLanguage = "ar";
    } else {
        selectedLanguage = "en";
    }
    setLanguage(selectedLanguage);
    localStorage.setItem("lang", selectedLanguage);
    updateSlider();
}

function setLanguage(language) {
    const elements = document.querySelectorAll("[data-i18n]");
    console.log(elements);
    elements.forEach((element) => {
        const translationKey = element.getAttribute("data-i18n");
        console.log(translationKey);
        element.textContent = translations[language][translationKey];
    });
    document.dir = language === "ar" ? "rtl" : "ltr";
    document.documentElement.lang = language;
}

function renderSlides() {
    slides.forEach((slide) => {
        const slideDiv = document.createElement("div");
        slideDiv.classList.add("slide");
        slideDiv.innerHTML = `
      <div class="image-container">
        <img src="${slide.img}" alt="${slide.title}">
      </div>
    `;
        sliderContainer.appendChild(slideDiv);
    });
}

function renderDots() {
    slides.forEach((_, index) => {
        const dot = document.createElement("div");
        dot.classList.add("dot");
        dot.addEventListener("click", () => goToSlide(index));
        dotsContainer.appendChild(dot);
    });
}

function autoSlide() {
    currentSlide = (currentSlide + 1) % slides.length;
    updateSlider();
}

function goToSlide(index) {
    currentSlide = index;
    updateSlider();
}

function updateSlider() {
    const offsetDir = document.documentElement.lang === "ar" ? -1 : 1;
    const offset = -currentSlide * 100 * offsetDir;
    sliderContainer.style.transform = `translateX(${offset}%)`;
    const dots = dotsContainer.querySelectorAll(".dot");
    dots.forEach((dot, index) => {
        dot.classList.toggle("active", index === currentSlide);
    });
}

function openPopup() {
    popup.style.display = "flex";
}

function closePopup() {
    popup.style.display = "none";
}

function closePopupOnClickOutside(event) {
    if (event.target === popup) {
        popup.style.display = "none";
    }
}

//function validateForm(event) {
//    event.preventDefault();
//    const username = document.getElementById("username").value.trim();
//    const country = document.getElementById("country").value.trim();
//    const phone = document.getElementById("phone").value.trim();
//    const email = document.getElementById("email").value.trim();

//    if (!username) {
//        alert("Username is required");
//        return;
//    }

//    if (!country) {
//        alert("Country is required");
//        return;
//    }

//    if (!validatePhone(phone)) {
//        alert("Invalid phone number format");
//        return;
//    }

//    if (!validateEmail(email)) {
//        alert("Invalid email format");
//        return;
//    }

//    // send user data to server
//}

function validatePhone(phone) {
    const phonePattern = /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s/0-9]*$/;
    return phonePattern.test(phone);
}

function validateEmail(email) {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}