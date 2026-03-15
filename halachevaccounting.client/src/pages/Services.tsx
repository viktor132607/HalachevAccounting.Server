import { useTranslation } from "react-i18next"

export default function Services() {
    const { i18n } = useTranslation()
    const lang = i18n.language

    const pageWrap =
        "mx-auto w-full max-w-6xl px-4 py-10 sm:px-6 lg:px-8"

    const titleClass =
        "text-center text-4xl font-bold tracking-tight text-slate-900 sm:text-5xl"

    const listClass =
        "mt-10 grid gap-6"

    const itemClass =
        "rounded-[26px] bg-white p-6 shadow-[0_10px_30px_rgba(15,23,42,0.08)] transition hover:-translate-y-1 hover:shadow-[0_16px_40px_rgba(15,23,42,0.12)] sm:p-8"

    const headingClass =
        "mb-3 text-2xl font-bold text-slate-900"

    const textClass =
        "text-[15px] leading-8 text-slate-700 sm:text-base"

    return lang === "bg" ? (
        <div className={pageWrap}>
            <div className="mx-auto max-w-5xl">
                <h1 className={titleClass}>Предлагани услуги</h1>

                <div className={listClass}>
                    <div className={itemClass} id="accounting">
                        <h2 className={headingClass}>Счетоводни услуги на юридически и физически лица</h2>
                        <p className={textClass}>
                            Пълно счетоводно обслужване — текущо осчетоводяване, изготвяне на справки, отчети и документация според българското законодателство.
                        </p>
                    </div>

                    <div className={itemClass} id="admin">
                        <h2 className={headingClass}>Административни услуги</h2>
                        <p className={textClass}>
                            Изготвяне на документи, договори, уведомления и административна поддръжка за нуждите на Вашия бизнес.
                        </p>
                    </div>

                    <div className={itemClass} id="tax">
                        <h2 className={headingClass}>Данъчни услуги</h2>
                        <p className={textClass}>
                            Консултации и подготовка на данъчни декларации, оптимизация и съдействие при проверки от НАП.
                        </p>
                    </div>

                    <div className={itemClass} id="company">
                        <h2 className={headingClass}>Регистрация на фирми</h2>
                        <p className={textClass}>
                            Пълно съдействие при откриване на ЕООД, ООД, регистрация по ЗДДС и стартиране на бизнес дейност.
                        </p>
                    </div>

                    <div className={itemClass} id="payroll">
                        <h2 className={headingClass}>Обработка на заплати</h2>
                        <p className={textClass}>
                            Изчисляване на възнаграждения, осигуровки, изготвяне и подаване на декларации и платежни нареждания.
                        </p>
                    </div>

                    <div className={itemClass} id="vat">
                        <h2 className={headingClass}>Месечна ДДС обработка</h2>
                        <p className={textClass}>
                            Изготвяне и подаване на месечни справки-декларации по ДДС, дневници за покупки и продажби.
                        </p>
                    </div>

                    <div className={itemClass} id="bank">
                        <h2 className={headingClass}>Онлайн банкиране</h2>
                        <p className={textClass}>
                            Помощ при настройка и управление на електронно банкиране, извършване на плащания и следене на транзакции.
                        </p>
                    </div>

                    <div className={itemClass} id="analysis">
                        <h2 className={headingClass}>Финансов анализ</h2>
                        <p className={textClass}>
                            Анализ на финансовите резултати, разходи и ефективност с цел подобряване на бизнес процесите и вземане на информирани решения.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    ) : (
        <div className={pageWrap}>
            <div className="mx-auto max-w-5xl">
                <h1 className={titleClass}>Services Provided</h1>

                <div className={listClass}>
                    <div className={itemClass} id="accounting">
                        <h2 className={headingClass}>Accounting services for legal and private entities</h2>
                        <p className={textClass}>
                            Full accounting support — ongoing bookkeeping, preparation of reports, statements, and documentation
                            in accordance with Bulgarian legislation.
                        </p>
                    </div>

                    <div className={itemClass} id="admin">
                        <h2 className={headingClass}>Administrative services</h2>
                        <p className={textClass}>
                            Preparation of documents, contracts, notifications, and administrative support tailored to your business needs.
                        </p>
                    </div>

                    <div className={itemClass} id="tax">
                        <h2 className={headingClass}>Tax services</h2>
                        <p className={textClass}>
                            Consultations and preparation of tax declarations, tax optimization, and assistance during audits by the National Revenue Agency.
                        </p>
                    </div>

                    <div className={itemClass} id="company">
                        <h2 className={headingClass}>Company registration</h2>
                        <p className={textClass}>
                            Full support for establishing companies (Ltd., LLC), VAT registration, and starting a business activity.
                        </p>
                    </div>

                    <div className={itemClass} id="payroll">
                        <h2 className={headingClass}>Payroll processing</h2>
                        <p className={textClass}>
                            Salary calculations, social security contributions, preparation and submission of declarations and payment orders.
                        </p>
                    </div>

                    <div className={itemClass} id="vat">
                        <h2 className={headingClass}>Monthly VAT processing</h2>
                        <p className={textClass}>
                            Preparation and submission of monthly VAT returns, purchase/sales ledgers, and related documentation.
                        </p>
                    </div>

                    <div className={itemClass} id="bank">
                        <h2 className={headingClass}>Online banking</h2>
                        <p className={textClass}>
                            Assistance with setting up and managing e-banking, making payments, and tracking transactions.
                        </p>
                    </div>

                    <div className={itemClass} id="analysis">
                        <h2 className={headingClass}>Financial analysis</h2>
                        <p className={textClass}>
                            Analysis of financial performance, costs, and efficiency to help improve business processes and support informed decisions.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    )
}