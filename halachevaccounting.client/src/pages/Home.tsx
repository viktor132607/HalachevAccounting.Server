import { useTranslation } from "react-i18next"
import { Link } from "react-router-dom"

export default function Home() {
    const { i18n } = useTranslation()
    const lang = i18n.language

    const cardBase =
        "flex w-full max-w-[960px] min-h-[352px] flex-col items-center rounded-2xl bg-sky-200 px-6 py-8 text-center text-gray-900 no-underline shadow-[0_10px_15px_rgba(0,0,0,0.08)] transition duration-150 ease-in-out hover:scale-[1.02] hover:shadow-[0_16px_30px_rgba(0,0,0,0.12)]"

    const imageBase =
        "mb-4 h-40 w-[120px] rounded-[10px] bg-gray-400 bg-center bg-no-repeat"

    const titleBase =
        "mb-[14px] inline-block rounded-full bg-white/70 px-6 py-2.5 text-[18px] font-semibold leading-snug"

    const descBase =
        "mb-4 max-w-[680px] text-[15px] leading-[1.7] text-gray-700"

    const bulletsBase =
        "mb-4 list-disc space-y-1 pl-5 text-left text-[15px] leading-[1.7] text-gray-800"

    const tagBase =
        "inline-block rounded-full bg-white px-4 py-2 text-[13px] font-extrabold text-gray-900 shadow-sm"

    return lang === "bg" ? (
        <div className="mx-auto w-full max-w-7xl px-4 py-10 sm:px-6 lg:px-8">
            <div className="flex flex-col items-center gap-12">
                <section className="max-w-3xl text-center text-gray-800">
                    <h1 className="mb-4 inline-block rounded-full bg-sky-100 px-6 py-2 text-[18px] font-semibold text-gray-900 shadow-[0_4px_10px_rgba(0,0,0,0.06)]">
                        Счетоводно обслужване без компромиси
                    </h1>
                    <p className="m-0 text-base leading-[1.7]">
                        Професионално счетоводно обслужване, съобразено с българското законодателство,
                        международните стандарти и реалните нужди на бизнеса.
                    </p>
                </section>

                <section className="max-w-3xl text-center">
                    <h2 className="mb-4 inline-block rounded-full bg-sky-100 px-6 py-2 text-sm font-semibold text-gray-900 shadow-[0_4px_10px_rgba(0,0,0,0.06)]">
                        КАКВО ПРЕДЛАГАМ
                    </h2>
                    <p className="mx-auto max-w-[720px] text-[15px] leading-[1.7] text-gray-700">
                        Ясни процеси, предвидимост и сигурност. Работите с един счетоводен партньор,
                        който поема отговорност и комуникира директно с институциите.
                    </p>
                </section>

                <section className="flex w-full flex-col items-center gap-8">
                    <a href="/services#accounting" className={cardBase}>
                        <div
                            className={`${imageBase} bg-cover`}
                            style={{
                                backgroundImage:
                                    'url("https://sasaccountant.com/wp-content/uploads/2025/06/cooperation-analyst-chart-professional-paper-economics_1418-47-1024x683.jpg")',
                            }}
                        />
                        <div className={titleBase}>Счетоводни услуги на юридически и физически лица</div>
                        <p className={descBase}>Пълно счетоводно обслужване — текущо осчетоводяване, справки и отчети.</p>
                        <ul className={bulletsBase}>
                            <li>Текущо осчетоводяване</li>
                            <li>Справки и отчети</li>
                            <li>Документация по закон</li>
                            <li>Годишно приключване</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#admin" className={cardBase}>
                        <div
                            className={`${imageBase} bg-contain`}
                            style={{ backgroundImage: 'url("/images/ChatGPT Image 01.02.2026 г. 17_04_18.png")' }}
                        />
                        <div className={titleBase}>Административни услуги</div>
                        <p className={descBase}>Изготвяне на документи, договори и административна поддръжка.</p>
                        <ul className={bulletsBase}>
                            <li>Договори и уведомления</li>
                            <li>Административни справки</li>
                            <li>Поддръжка на документация</li>
                            <li>Координация по процеси</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#tax" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Данъчни услуги</div>
                        <p className={descBase}>Консултации и подготовка на декларации, оптимизация и съдействие.</p>
                        <ul className={bulletsBase}>
                            <li>Данъчни декларации</li>
                            <li>Данъчно планиране</li>
                            <li>Оптимизация</li>
                            <li>Съдействие при проверки</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#company" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Регистрация на фирми</div>
                        <p className={descBase}>Пълно съдействие при откриване на фирма и регистрация по ЗДДС.</p>
                        <ul className={bulletsBase}>
                            <li>ЕООД / ООД</li>
                            <li>Промени в ТР</li>
                            <li>Регистрация по ЗДДС</li>
                            <li>Стартова консултация</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#payroll" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Обработка на заплати</div>
                        <p className={descBase}>ТРЗ — възнаграждения, осигуровки, декларации и платежни нареждания.</p>
                        <ul className={bulletsBase}>
                            <li>Заплати и осигуровки</li>
                            <li>Декларации</li>
                            <li>Платежни нареждания</li>
                            <li>Кадрови документи</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#vat" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Месечна ДДС обработка</div>
                        <p className={descBase}>Справки-декларации по ДДС и дневници за покупки и продажби.</p>
                        <ul className={bulletsBase}>
                            <li>ДДС дневници</li>
                            <li>Справка-декларация</li>
                            <li>VIES</li>
                            <li>Контрол на документи</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#bank" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Онлайн банкиране</div>
                        <p className={descBase}>Помощ при e-banking, плащания и проследяване на транзакции.</p>
                        <ul className={bulletsBase}>
                            <li>Плащания</li>
                            <li>Извлечения</li>
                            <li>Съгласуване</li>
                            <li>Контрол на транзакции</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>

                    <a href="/services#analysis" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Финансов анализ</div>
                        <p className={descBase}>Анализ на резултати, разходи и ефективност за по-добри решения.</p>
                        <ul className={bulletsBase}>
                            <li>Анализ на разходи</li>
                            <li>Рентабилност</li>
                            <li>Парични потоци</li>
                            <li>Управленски отчети</li>
                        </ul>
                        <div><span className={tagBase}>Виж повече</span></div>
                    </a>
                </section>

                <section className="flex max-w-3xl flex-col items-center text-center">
                    <h3 className="mb-3 text-2xl font-bold text-gray-900">Търсите надежден счетоводен партньор?</h3>
                    <p className="mb-6 text-[15px] leading-[1.7] text-gray-700">
                        Работите спокойно, с яснота и сигурност, докато счетоводството е под контрол.
                    </p>
                    <Link
                        to="/contact"
                        className="inline-flex items-center rounded-full bg-sky-500 px-6 py-3 text-sm font-bold text-white transition hover:bg-sky-600"
                    >
                        Свържи се
                    </Link>
                </section>
            </div>
        </div>
    ) : (
        <div className="mx-auto w-full max-w-7xl px-4 py-10 sm:px-6 lg:px-8">
            <div className="flex flex-col items-center gap-12">
                <section className="max-w-3xl text-center text-gray-800">
                    <h1 className="mb-4 inline-block rounded-full bg-sky-100 px-6 py-2 text-[18px] font-semibold text-gray-900 shadow-[0_4px_10px_rgba(0,0,0,0.06)]">
                        Reliable Accounting Services
                    </h1>
                    <p className="m-0 text-base leading-[1.7]">
                        Professional accounting services aligned with Bulgarian legislation and international standards.
                        You work with a dedicated accounting partner who ensures accuracy, compliance, and clear financial control.
                    </p>
                </section>

                <section className="max-w-3xl text-center">
                    <h2 className="mb-4 inline-block rounded-full bg-sky-100 px-6 py-2 text-sm font-semibold text-gray-900 shadow-[0_4px_10px_rgba(0,0,0,0.06)]">
                        WHAT I OFFER
                    </h2>
                    <p className="mx-auto max-w-[720px] text-[15px] leading-[1.7] text-gray-700">
                        Clear processes, predictability, and security. You receive accounting tailored to your business,
                        direct communication, and full support in dealings with institutions such as the National Revenue Agency.
                    </p>
                </section>

                <section className="flex w-full flex-col items-center gap-8">
                    <a href="/services#accounting" className={cardBase}>
                        <div
                            className={`${imageBase} bg-cover`}
                            style={{
                                backgroundImage:
                                    'url("https://sasaccountant.com/wp-content/uploads/2025/06/cooperation-analyst-chart-professional-paper-economics_1418-47-1024x683.jpg")',
                            }}
                        />
                        <div className={titleBase}>Accounting services for companies and individuals</div>
                        <p className={descBase}>Complete accounting services ensuring compliance and financial clarity.</p>
                        <ul className={bulletsBase}>
                            <li>Ongoing bookkeeping</li>
                            <li>Financial statements</li>
                            <li>Annual closing</li>
                            <li>Accounting consultations</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#admin" className={cardBase}>
                        <div
                            className={`${imageBase} bg-contain`}
                            style={{ backgroundImage: 'url("/images/ChatGPT Image 01.02.2026 г. 17_04_18.png")' }}
                        />
                        <div className={titleBase}>Administrative services</div>
                        <p className={descBase}>Administrative support and representation before authorities.</p>
                        <ul className={bulletsBase}>
                            <li>Communication with NRA & NSSI</li>
                            <li>Submission of declarations</li>
                            <li>Document management</li>
                            <li>Administrative reports</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#tax" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Tax services</div>
                        <p className={descBase}>Accurate tax planning and compliance.</p>
                        <ul className={bulletsBase}>
                            <li>Tax consultations</li>
                            <li>Tax optimization</li>
                            <li>Annual tax returns</li>
                            <li>Support during audits</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#company" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Company registration</div>
                        <p className={descBase}>Fast and compliant company setup.</p>
                        <ul className={bulletsBase}>
                            <li>Ltd / LLC registration</li>
                            <li>Sole traders & freelancers</li>
                            <li>Registry changes</li>
                            <li>Startup consulting</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#payroll" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Payroll processing</div>
                        <p className={descBase}>Full payroll and HR administration.</p>
                        <ul className={bulletsBase}>
                            <li>Employment contracts</li>
                            <li>Salaries & social security</li>
                            <li>Monthly declarations</li>
                            <li>Leave & sick leave handling</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#vat" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Monthly VAT processing</div>
                        <p className={descBase}>Timely and accurate VAT reporting.</p>
                        <ul className={bulletsBase}>
                            <li>VAT ledgers</li>
                            <li>VAT returns</li>
                            <li>VIES declarations</li>
                            <li>EU transactions</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#bank" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Online banking</div>
                        <p className={descBase}>Support and control of banking operations.</p>
                        <ul className={bulletsBase}>
                            <li>Payment orders</li>
                            <li>Bank reconciliation</li>
                            <li>Account monitoring</li>
                            <li>Cash flow control</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>

                    <a href="/services#analysis" className={cardBase}>
                        <div className={`${imageBase} bg-cover`} />
                        <div className={titleBase}>Financial analysis</div>
                        <p className={descBase}>Insight and control over financial performance.</p>
                        <ul className={bulletsBase}>
                            <li>Cost analysis</li>
                            <li>Cash flow analysis</li>
                            <li>Profitability</li>
                            <li>Management reports</li>
                        </ul>
                        <div><span className={tagBase}>Learn more</span></div>
                    </a>
                </section>

                <section className="flex max-w-3xl flex-col items-center text-center">
                    <h3 className="mb-3 text-2xl font-bold text-gray-900">Looking for a reliable accounting partner?</h3>
                    <p className="mb-6 text-[15px] leading-[1.7] text-gray-700">
                        Work with confidence, clarity, and full financial control.
                    </p>
                    <Link
                        to="/contact"
                        className="inline-flex items-center rounded-full bg-sky-500 px-6 py-3 text-sm font-bold text-white transition hover:bg-sky-600"
                    >
                        Contact me
                    </Link>
                </section>
            </div>
        </div>
    )
}