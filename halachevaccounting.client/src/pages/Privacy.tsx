import { useTranslation } from "react-i18next"

export default function Privacy() {
    const { i18n } = useTranslation()
    const lang = i18n.language

    const wrapperClass =
        "mx-auto w-full max-w-5xl px-4 py-10 sm:px-6 lg:px-8"

    const titleClass =
        "text-center text-4xl font-bold tracking-tight text-slate-900 sm:text-5xl"

    const updatedClass =
        "mt-3 text-center text-sm font-medium text-slate-500"

    const sectionClass =
        "mt-8 rounded-[26px] bg-white p-6 shadow-[0_10px_30px_rgba(15,23,42,0.08)] sm:p-8"

    const headingClass =
        "mb-4 text-2xl font-bold text-slate-900"

    const textClass =
        "text-[15px] leading-8 text-slate-700 sm:text-base"

    const boxClass =
        "my-5 rounded-[22px] bg-sky-50 p-5 text-[15px] leading-8 text-slate-700"

    const listClass =
        "mt-4 list-disc space-y-3 pl-5 text-[15px] leading-8 text-slate-700 marker:text-sky-500 sm:text-base"

    return lang === "bg" ? (
        <div className={wrapperClass}>
            <h1 className={titleClass}>Политика за поверителност</h1>
            <p className={updatedClass}>
                Последна актуализация: <span className="font-semibold text-slate-700">16.11.2025</span>
            </p>

            <section className={sectionClass}>
                <div className="space-y-4">
                    <p className={textClass}>
                        Настоящата политика за поверителност описва как „РЕПАРО НАУ“ ЕООД („Репаро“, „ние“) обработва лични данни при предоставяне на мобилни автосервизни услуги, уебсайта reparonow.com и свързаните канали.
                    </p>
                    <p className={textClass}>
                        Поддържаме прозрачност и прилагаме мерки, които отговарят на изискванията на Общия регламент за защита на данните (GDPR) и Закона за защита на личните данни.
                    </p>
                </div>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Администратор на лични данни</h2>
                <p className={textClass}>
                    „РЕПАРО НАУ“ ЕООД е администратор на личните ви данни, когато използвате нашия уебсайт, заявявате услуга или общувате с нас.
                </p>

                <div className={boxClass}>
                    <p><strong>Юридическо лице:</strong> „РЕПАРО НАУ“ ЕООД</p>
                    <p><strong>Седалище и адрес на управление:</strong> България, Русе, кв. Средна Кула, ул. „Бели Лом“ 2 (бившите халета на фирма Кракра)</p>
                    <p><strong>Електронна поща:</strong> contact@reparonow.com</p>
                    <p><strong>Телефон:</strong> +359 87 885 0340 / +359 87 783 1309</p>
                </div>

                <p className={textClass}>
                    За въпроси относно защитата на данните можете да се свържете с нас чрез посочените по-горе канали.
                </p>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Видове лични данни, които обработваме</h2>
                <p className={textClass}>
                    Събираме ограничено количество лични данни, които са необходими за изпълнение на услугите ни и за спазване на законовите ни задължения.
                </p>

                <ul className={listClass}>
                    <li><strong>Идентификационни данни:</strong> име, фамилия, телефон и имейл адрес.</li>
                    <li><strong>Данни за услуги:</strong> информация за превозното средство, локация, предпочитан час, съдържание на запитване.</li>
                    <li><strong>Финансови и счетоводни данни:</strong> данни от фактури и платежни документи.</li>
                    <li><strong>Технически данни:</strong> логове, данни за устройството, бисквитки за сигурност.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Правни основания за обработване</h2>
                <p className={textClass}>
                    Опирайки се на чл. 6 от GDPR, обработваме личните данни на следните основания:
                </p>

                <ul className={listClass}>
                    <li>Изпълнение на договор: за да обработим заявката ви и да предоставим желаната услуга.</li>
                    <li>Законно задължение: за счетоводство, фактуриране и спазване на нормативните изисквания.</li>
                    <li>Законен интерес: за подобряване на услугите, предотвратяване на злоупотреби и защита на мрежова сигурност.</li>
                    <li>Съгласие: за маркетинг, бюлетини и незадължителни бисквитки.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Как използваме личните данни</h2>
                <p className={textClass}>
                    Използваме личните данни само за конкретни и ясно определени цели:
                </p>

                <ul className={listClass}>
                    <li>Обработка на запитвания и резервации.</li>
                    <li>Предоставяне на мобилни и сервизни услуги.</li>
                    <li>Комуникация, поддръжка и известия.</li>
                    <li>Маркетинг – само при дадено съгласие.</li>
                    <li>Анализ и персонализиране на съдържанието.</li>
                    <li>Сигурност и защита от злоупотреби.</li>
                    <li>Спазване на законови задължения.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Споделяне на лични данни</h2>
                <p className={textClass}>
                    Не продаваме лични данни. Споделяме ги само когато е необходимо за услугата или по закон.
                </p>

                <ul className={listClass}>
                    <li>Хостинг и ИТ поддръжка.</li>
                    <li>Плащания и счетоводни услуги.</li>
                    <li>Комуникационни системи (имейл, SMS, чат).</li>
                    <li>Партньори и подизпълнители.</li>
                    <li>Държавни органи – при законово основание.</li>
                </ul>

                <p className={`${textClass} mt-4`}>
                    Всички доставчици работят съгласно договори и инструкции от „РЕПАРО НАУ“ ЕООД.
                </p>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Съхранение и срокове</h2>
                <p className={textClass}>
                    Съхраняваме данните само толкова дълго, колкото е необходимо за целите, за които са събрани, или според закона.
                </p>

                <ul className={listClass}>
                    <li>Запитвания: до 24 месеца.</li>
                    <li>Договори и документи: до 5 години.</li>
                    <li>Маркетинг: до оттегляне на съгласието или 24 месеца без активност.</li>
                    <li>Технически логове: до 12 месеца.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Вашите права</h2>
                <p className={textClass}>
                    Като субект на данни имате следните права съгласно GDPR:
                </p>

                <ul className={listClass}>
                    <li>Право на достъп</li>
                    <li>Право на корекция</li>
                    <li>Право на изтриване</li>
                    <li>Право на ограничаване</li>
                    <li>Право на преносимост</li>
                    <li>Право на възражение</li>
                    <li>Право да оттеглите съгласие</li>
                    <li>Право на жалба до КЗЛД</li>
                </ul>

                <p className={`${textClass} mt-4`}>
                    Упражняване на права: contact@reparonow.com
                </p>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Бисквитки и проследяване</h2>
                <div className="space-y-4">
                    <p className={textClass}>Използваме бисквитки за функционалност, сигурност и анализ.</p>
                    <p className={textClass}>Подробна информация можете да намерите в отделната политика за бисквитки.</p>
                </div>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Промени в политиката</h2>
                <div className="space-y-4">
                    <p className={textClass}>Актуализираме политиката при промени в практиките или закона.</p>
                    <p className={textClass}>
                        Новата версия влиза в сила от датата на публикуването ѝ на тази страница. Ще ви уведомим за съществени промени чрез подходящи канали.
                    </p>
                </div>
            </section>
        </div>
    ) : (
        <div className={wrapperClass}>
            <h1 className={titleClass}>Privacy Policy</h1>
            <p className={updatedClass}>
                Last updated: <span className="font-semibold text-slate-700">16.11.2025</span>
            </p>

            <section className={sectionClass}>
                <div className="space-y-4">
                    <p className={textClass}>
                        This privacy policy explains how Reparo Now EOOD (“Reparo”, “we”) processes personal data when delivering mobile automotive services, operating the reparonow.com website, and interacting through related channels.
                    </p>
                    <p className={textClass}>
                        We remain transparent about our practices and implement safeguards that comply with the General Data Protection Regulation (GDPR) and the Bulgarian Personal Data Protection Act.
                    </p>
                </div>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Data Controller</h2>
                <p className={textClass}>
                    Reparo Now EOOD acts as the data controller whenever you use our website, request a service, or communicate with us.
                </p>

                <div className={boxClass}>
                    <p><strong>Registered entity:</strong> Reparo Now EOOD</p>
                    <p><strong>Registered address:</strong> Bulgaria, Ruse, Sredna Kula District, Beli Lom St. 2 (former Kraka warehouses)</p>
                    <p><strong>Email:</strong> contact@reparonow.com</p>
                    <p><strong>Phone:</strong> +359 87 885 0340 / +359 87 783 1309</p>
                </div>

                <p className={textClass}>
                    If you have any questions about privacy or data protection, get in touch through the channels above.
                </p>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Types of Personal Data We Process</h2>
                <p className={textClass}>
                    We collect only the personal data that is necessary to deliver our services and to meet our legal obligations.
                </p>

                <ul className={listClass}>
                    <li><strong>Identification data:</strong> name, surname, phone number, and email address provided in contact or booking forms.</li>
                    <li><strong>Service information:</strong> vehicle details, preferred service location, preferred time slot, and the content of your request.</li>
                    <li><strong>Financial and accounting data:</strong> details contained in invoices and payment documents where applicable.</li>
                    <li><strong>Technical data:</strong> website logs, device information, and cookies required for security and essential functionality.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Legal Grounds for Processing</h2>
                <p className={textClass}>
                    In line with Article 6 of the GDPR, we process personal data on the following legal grounds:
                </p>

                <ul className={listClass}>
                    <li>Performance of a contract: to process your request and deliver the service you booked.</li>
                    <li>Legal obligation: to comply with accounting, tax, and other regulatory obligations.</li>
                    <li>Legitimate interest: to improve our services, prevent abuse, and safeguard network security.</li>
                    <li>Consent: when you explicitly agree to marketing communications or optional cookies.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>How We Use Personal Data</h2>
                <p className={textClass}>
                    We use personal data for clearly defined purposes:
                </p>

                <ul className={listClass}>
                    <li>Handling enquiries and bookings: communicating with you, confirming details, and scheduling visits.</li>
                    <li>Delivering mobile and workshop services: organising field teams, maintaining service records, and honouring warranties.</li>
                    <li>Customer communication and support: answering questions, sending notifications, and providing follow-up information.</li>
                    <li>Marketing and personalisation: only with prior consent – sending newsletters and analysing campaign performance.</li>
                    <li>Security and fraud prevention: monitoring traffic to protect against malicious activity.</li>
                    <li>Compliance with legal obligations.</li>
                    <li>Improving service quality and user experience.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Sharing of Personal Data</h2>
                <p className={textClass}>
                    We do not sell personal data to third parties. We share information only when required to provide the service or to comply with the law.
                </p>

                <ul className={listClass}>
                    <li>Hosting and IT support: cloud infrastructure and maintenance providers.</li>
                    <li>Payments and accounting: financial institutions, accountants, and tax advisors.</li>
                    <li>Communication platforms: systems that deliver email, SMS, or chat messages.</li>
                    <li>Partners and subcontractors: carefully selected partners that perform a specific service on our behalf.</li>
                    <li>Public authorities: when required by applicable legislation or upon lawful request.</li>
                </ul>

                <p className={`${textClass} mt-4`}>
                    Each service provider processes personal data under a written agreement and follows our documented instructions.
                </p>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Data Retention</h2>
                <p className={textClass}>
                    We retain personal data only for as long as needed to fulfil the purposes outlined in this policy or to comply with legal requirements.
                </p>

                <ul className={listClass}>
                    <li>Enquiries and correspondence: up to 24 months after the last interaction.</li>
                    <li>Contracts and service records: up to 5 years in line with accounting regulations.</li>
                    <li>Marketing preferences: until you withdraw consent or after 24 months of inactivity.</li>
                    <li>Technical logs: up to 12 months for security auditing.</li>
                </ul>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Your Rights</h2>
                <p className={textClass}>
                    Under the GDPR you have the following rights over your personal data:
                </p>

                <ul className={listClass}>
                    <li>Right of access</li>
                    <li>Right to rectification</li>
                    <li>Right to erasure</li>
                    <li>Right to restriction</li>
                    <li>Right to data portability</li>
                    <li>Right to object</li>
                    <li>Right to withdraw consent</li>
                    <li>Right to lodge a complaint with the Bulgarian Commission for Personal Data Protection</li>
                </ul>

                <p className={`${textClass} mt-4`}>
                    To exercise your rights, email us at contact@reparonow.com or write to the address above. We will respond within one month.
                </p>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Cookies and Tracking Technologies</h2>
                <div className="space-y-4">
                    <p className={textClass}>
                        We use cookies to ensure the website operates correctly, understand how visitors use it, and tailor content.
                    </p>
                    <p className={textClass}>
                        Full details about cookie categories and how to manage your preferences are available in our separate Cookie Policy.
                    </p>
                </div>
            </section>

            <section className={sectionClass}>
                <h2 className={headingClass}>Changes to This Policy</h2>
                <div className="space-y-4">
                    <p className={textClass}>
                        We update this policy whenever our practices change or when legislation requires it.
                    </p>
                    <p className={textClass}>
                        The new version takes effect once published on this page. We will notify you of major updates through appropriate channels.
                    </p>
                </div>
            </section>
        </div>
    )
}