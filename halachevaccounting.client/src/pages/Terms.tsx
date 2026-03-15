import { useTranslation } from "react-i18next"

export default function Terms() {
    const { i18n } = useTranslation()
    const lang = i18n.language

    const wrapperClass =
        "mx-auto w-full max-w-5xl px-4 py-10 sm:px-6 lg:px-8"

    const titleClass =
        "text-center text-4xl font-bold tracking-tight text-slate-900 sm:text-5xl"

    const updatedClass =
        "mt-3 text-center text-sm font-medium text-slate-500"

    const cardClass =
        "mt-8 rounded-[26px] bg-white p-6 shadow-[0_10px_30px_rgba(15,23,42,0.08)] sm:p-8"

    const headingClass =
        "mt-8 mb-4 text-2xl font-bold text-slate-900 first:mt-0"

    const textClass =
        "text-[15px] leading-8 text-slate-700 sm:text-base"

    const groupClass = "space-y-4"

    return lang === "bg" ? (
        <div className={wrapperClass}>
            <h1 className={titleClass}>Общи условия</h1>
            <p className={updatedClass}>
                Последна актуализация: <span className="font-semibold text-slate-700">14 октомври 2025 г.</span>
            </p>

            <div className={cardClass}>
                <h2 className={headingClass}>Въведение</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Настоящите Общи условия уреждат мобилните автосервизни услуги, предоставяни от „РЕПАРО НАУ“ ЕООД („Репаро“, „ние“), както и използването на уебсайта reparonow.com.
                    </p>
                    <p className={textClass}>
                        С изпращане на заявка, потвърждение на час или използване на сайта сключвате договор от разстояние съгласно българското законодателство и приемате тези условия.
                    </p>
                </div>

                <h2 className={headingClass}>Основни понятия</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        „РЕПАРО НАУ“ ЕООД е дружество, регрегистрирано в България, което предоставя услуги съгласно действащото законодателство, включително Закона за защита на потребителите и Закона за задълженията и договорите.
                    </p>
                    <p className={textClass}>
                        Условията се прилагат както за физически лица, така и за юридически лица. Когато представлявате дружество, потвърждавате, че имате необходимите правомощия.
                    </p>
                    <p className={textClass}>
                        <strong>Доставчик на услугата:</strong> „РЕПАРО НАУ“ ЕООД, извършващо мобилна диагностика, ремонти, пътна помощ и свързани услуги.
                    </p>
                    <p className={textClass}>
                        <strong>Платформа:</strong> уебсайтът reparonow.com, контактни форми, телефонни линии, чатове и дигитални инструменти.
                    </p>
                    <p className={textClass}>
                        <strong>Клиент:</strong> всяко физическо или юридическо лице, което заявява, резервира или получава услуги от Reparo.
                    </p>
                </div>

                <h2 className={headingClass}>Обхват на услугите</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Предоставяме мобилна диагностика, ремонти, пътна помощ, обслужване на гуми и други автомобилни услуги, описани на сайта или договорени писмено.
                    </p>
                    <p className={textClass}>
                        Наличността зависи от местоположението, вида и състоянието на превозното средство. Можем да откажем услуги, които са извън техническите ни възможности или законовите изисквания.
                    </p>
                    <p className={textClass}>
                        Когато услугата изисква работа в сервиз или доставка на части от трети страни, предварително ви информираме за сроковете и допълнителните условия.
                    </p>
                </div>

                <h2 className={headingClass}>Права и задължения</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        <strong>Записване и възникване на договора:</strong> Заявки можете да изпращате чрез сайта, имейл, телефон или чат. Договор възниква, когато потвърдим посещението с дата, час и обхват.
                    </p>
                    <p className={textClass}>
                        <strong>Необходима информация:</strong> марка, модел, регистрационен номер, локация, описание на проблема и предпочитан час.
                    </p>
                    <p className={textClass}>
                        <strong>Потвърждение:</strong> изпращаме ви потвърждение с всички необходими данни.
                    </p>
                </div>

                <h2 className={headingClass}>Плащане и условия</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Посочените цени включват ДДС, освен ако изрично не е указано друго.
                    </p>
                    <p className={textClass}>
                        Ако по време на диагностика се установи нужда от допълнителни услуги или части, представяме актуализирана оферта и изчакваме вашето одобрение.
                    </p>
                    <p className={textClass}>
                        Плащането се дължи след приключване на услугата, освен ако не е уговорено друго.
                    </p>
                    <p className={textClass}>
                        <strong>Начини на плащане:</strong> в брой, карта, банков път или други методи, обявени на платформата.
                    </p>
                    <p className={textClass}>
                        <strong>Такса посещение:</strong> възможно е начисляване, ако техникът е пристигнал, но услугата не може да бъде извършена по причини извън наш контрол.
                    </p>
                </div>

                <h2 className={headingClass}>Отказ и прекратяване</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Можете да отмените или промените посещение без такса до 24 часа преди уговорения час.
                    </p>
                    <p className={textClass}>
                        При спешни услуги приемате, че правото на отказ отпада след пълното изпълнение (чл. 57 ЗЗП).
                    </p>
                    <p className={textClass}>
                        При отмяна от наша страна поради безопасност или форсмажор възстановяваме всички предварително платени суми.
                    </p>
                    <p className={textClass}>
                        <strong>Късни промени:</strong> при отмяна под 24 часа може да се начисли такса посещение или разходи за поръчани части.
                    </p>
                    <p className={textClass}>
                        <strong>Липса на достъп:</strong> ако автомобилът е недостъпен, може да начислим пълната такса за посещение.
                    </p>
                </div>

                <h2 className={headingClass}>Защита на личните данни</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Обработваме лични данни единствено съгласно нашата Политика за поверителност и Политика за бисквитките.
                    </p>
                    <p className={textClass}>
                        С изпращане на заявка потвърждавате, че сте се запознали с тях и предоставяте точна информация.
                    </p>
                </div>

                <h2 className={headingClass}>Отговорност</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Носим отговорност за преки вреди, причинени по наша вина, в рамките на българското законодателство.
                    </p>
                    <p className={textClass}>
                        Не носим отговорност за косвени вреди като пропуснати ползи, срещи или загуба на данни.
                    </p>
                    <p className={textClass}>
                        Не носим отговорност за ценности, оставени в автомобила, освен ако не са декларирани и приети писмено.
                    </p>
                </div>

                <h2 className={headingClass}>Приложимо право</h2>
                <div className={groupClass}>
                    <p className={textClass}>Тези условия се регулират от законите на Република България.</p>
                    <p className={textClass}>
                        Спорове могат да бъдат отнесени към КЗП или платформата за онлайн решаване на спорове на ЕС.
                    </p>
                    <p className={textClass}>
                        Компетентни са българските съдилища, освен ако законът предвижда друго.
                    </p>
                </div>

                <h2 className={headingClass}>Контакти</h2>
                <div className={groupClass}>
                    <p className={textClass}><strong>Юридическо лице:</strong> „РЕПАРО НАУ“ ЕООД</p>
                    <p className={textClass}>
                        <strong>Адрес:</strong> България, Русе, кв. Средна Кула, ул. „Бели Лом“ 2 (бившите халета на фирма Кракра)
                    </p>
                    <p className={textClass}><strong>Имейл:</strong> contact@reparonow.com</p>
                    <p className={textClass}><strong>Телефон:</strong> +359 87 885 0340 / +359 87 783 1309</p>
                </div>
            </div>
        </div>
    ) : (
        <div className={wrapperClass}>
            <h1 className={titleClass}>Terms of Service</h1>
            <p className={updatedClass}>
                Last updated: <span className="font-semibold text-slate-700">14 October 2025</span>
            </p>

            <div className={cardClass}>
                <h2 className={headingClass}>Introduction</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        These Terms of Service govern the mobile automotive services provided by Reparo Now EOOD (“Reparo”, “we”) and the use of reparonow.com.
                    </p>
                    <p className={textClass}>
                        By submitting a booking request, confirming an appointment, or using our website you enter into a binding distance contract under Bulgarian law and agree to comply with these terms.
                    </p>
                </div>

                <h2 className={headingClass}>Definitions</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Reparo Now EOOD is a company registered in Bulgaria and provides services in accordance with national legislation, including the Consumer Protection Act and the Obligations and Contracts Act.
                    </p>
                    <p className={textClass}>
                        These terms apply to both private individuals and legal entities. When you represent a company you confirm that you are authorised to bind that company to these terms.
                    </p>
                    <p className={textClass}>
                        <strong>Service Provider:</strong> Reparo Now EOOD, delivering mobile diagnostics, repairs, roadside assistance, and related automotive services across Bulgaria.
                    </p>
                    <p className={textClass}>
                        <strong>Platform:</strong> the website reparonow.com, contact forms, phone lines, chat, and any digital tools used to request or manage services.
                    </p>
                    <p className={textClass}>
                        <strong>Customer:</strong> any natural person or entity that requests, books, or receives services from Reparo, including their authorised representatives.
                    </p>
                </div>

                <h2 className={headingClass}>Scope of Services</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        We deliver on-site diagnostics, repairs, roadside assistance, tyre services, and other automotive solutions as described on the website or agreed in writing.
                    </p>
                    <p className={textClass}>
                        Availability depends on location, vehicle type, and the condition of the vehicle. We may decline requests that fall outside our technical capabilities or legal obligations.
                    </p>
                    <p className={textClass}>
                        If the service requires workshop work or third-party parts, we will inform you about the expected timelines and any additional terms before proceeding.
                    </p>
                </div>

                <h2 className={headingClass}>Rights & Responsibilities</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        <strong>Booking Procedure and Contract Formation:</strong> You can submit booking requests through our website, by email, chat, or by calling the numbers published on the platform. A binding contract is formed once we confirm the appointment with date, time, and scope of work.
                    </p>
                    <p className={textClass}>
                        <strong>Required Information:</strong> vehicle make and model, registration number, current location, issue description, and preferred time slot.
                    </p>
                    <p className={textClass}>
                        <strong>Confirmation Message:</strong> our team will send a confirmation outlining the agreed visit and any prerequisites before the service is performed.
                    </p>
                </div>

                <h2 className={headingClass}>Pricing & Payment</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        Unless otherwise stated, quoted prices include VAT and all mandatory fees.
                    </p>
                    <p className={textClass}>
                        If an initial diagnosis reveals the need for additional work or parts, we will present a revised estimate and obtain your approval before continuing.
                    </p>
                    <p className={textClass}>
                        Payment is due immediately after completion of the service unless alternative terms are agreed upon in writing.
                    </p>
                    <p className={textClass}>
                        <strong>Payment methods:</strong> cash, debit or credit card, bank transfer, or other methods announced on the platform.
                    </p>
                    <p className={textClass}>
                        <strong>Call-out and diagnostic fees:</strong> a non-refundable fee may apply when a technician attends the location but the service cannot be completed for reasons beyond our control.
                    </p>
                </div>

                <h2 className={headingClass}>Cancellation & Termination</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        You may request to reschedule or cancel a confirmed appointment free of charge up to 24 hours before the agreed time.
                    </p>
                    <p className={textClass}>
                        For urgent repairs performed during the withdrawal period, you acknowledge that the right of withdrawal under Article 57(1), item 1 of the Consumer Protection Act no longer applies once the service has been fully delivered.
                    </p>
                    <p className={textClass}>
                        If we must cancel due to safety, weather conditions, or force majeure, we will notify you promptly and refund any prepaid amounts.
                    </p>
                    <p className={textClass}>
                        <strong>Late changes:</strong> cancellations less than 24 hours before the appointment may result in a call-out fee or costs for already ordered parts.
                    </p>
                    <p className={textClass}>
                        <strong>Customer no-show:</strong> if the vehicle is unavailable or access is denied, we may charge the full call-out fee and reschedule based on availability.
                    </p>
                </div>

                <h2 className={headingClass}>Privacy & Data Protection</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        We process personal data and use cookies strictly in line with our Privacy Policy and Cookie Policy.
                    </p>
                    <p className={textClass}>
                        By requesting a service you confirm that you have read those policies and that the data you provide is accurate and lawful.
                    </p>
                </div>

                <h2 className={headingClass}>Liability</h2>
                <div className={groupClass}>
                    <p className={textClass}>
                        We are liable for direct damages resulting from intentional misconduct or gross negligence by our staff, as required by Bulgarian law.
                    </p>
                    <p className={textClass}>
                        To the fullest extent permitted by law we are not responsible for indirect or consequential losses such as lost profits, missed appointments, or loss of data.
                    </p>
                    <p className={textClass}>
                        Nothing in these terms limits your statutory consumer rights or our liability when such limitation is prohibited by law.
                    </p>
                    <p className={textClass}>
                        <strong>Vehicle contents:</strong> Reparo is not responsible for valuables left in the vehicle unless expressly declared and accepted in writing.
                    </p>
                </div>

                <h2 className={headingClass}>Governing Law</h2>
                <div className={groupClass}>
                    <p className={textClass}>These terms are governed by the laws of the Republic of Bulgaria.</p>
                    <p className={textClass}>
                        Consumers may refer disputes to the Commission for Consumer Protection or the EU Online Dispute Resolution platform at https://ec.europa.eu/consumers/odr.
                    </p>
                    <p className={textClass}>
                        Bulgarian courts have jurisdiction unless mandatory rules entitle you to initiate proceedings elsewhere.
                    </p>
                </div>

                <h2 className={headingClass}>Contact</h2>
                <div className={groupClass}>
                    <p className={textClass}><strong>Registered entity:</strong> Reparo Now EOOD</p>
                    <p className={textClass}>
                        <strong>Registered address:</strong> Bulgaria, Ruse, Sredna Kula District, Beli Lom St. 2 (former Kraka warehouses)
                    </p>
                    <p className={textClass}><strong>Email:</strong> contact@reparonow.com</p>
                    <p className={textClass}><strong>Phone:</strong> +359 87 885 0340 / +359 87 783 1309</p>
                </div>
            </div>
        </div>
    )
}