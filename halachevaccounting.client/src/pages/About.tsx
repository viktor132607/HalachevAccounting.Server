import { useTranslation } from "react-i18next"

export default function About() {
    const { i18n } = useTranslation()
    const isBg = i18n.language?.toLowerCase().startsWith("bg")

    const textClass = "text-[15px] leading-[2] text-[#4b5563]"
    const sectionLine = "my-8 h-px w-full bg-[#d9d9d9]"
    const triangle = "mt-[9px] h-0 w-0 border-l-[8px] border-l-red-500 border-y-[6px] border-y-transparent"

    return (
        <div className="mx-auto w-full max-w-[860px] px-4 pb-16 pt-10">
            <div className="flex flex-col items-center">
                <img
                    src="/images/fuckinghell.png"
                    alt="Nikola Halachev"
                    className="w-full max-w-[720px] border border-[#d9d9d9] object-cover"
                />

                {isBg ? (
                    <div className="mt-6 w-full max-w-[720px]">
                        <h1 className="mb-6 text-center text-[22px] font-bold text-[#1f2937]">
                            Никола Халачев - Счетоводител
                        </h1>

                        <div className="space-y-5">
                            <p className={textClass}>
                                Всеки ден имам задачата да работя усилено и качествено, като затвърждавам и надграждам знанията и уменията, които съм натрупал до момента.
                            </p>

                            <p className={textClass}>
                                Целта ми е да достигна максимално много хора, които се нуждаят от счетоводни услуги и да помагам. Вярвам, че това ще се случи!
                            </p>

                            <p className={textClass}>
                                Възприел съм за мисия, това което правя. И влагам всичко от себе си в развитието си и това на хората около мен. Искам всеки, който работи с мен, а и не само, да бъде най-добрата версия на себе си.
                            </p>

                            <p className={textClass}>
                                Имам име, което тепърва ще градя и затова не смятам да разочаровам нито един свой клиент.
                            </p>
                        </div>

                        <div className={sectionLine} />

                        <div>
                            <p className="mb-4 text-[15px] font-medium leading-[2] text-[#4b5563]">
                                Професионалната си квалификация придобих в ИУ-Варна:
                            </p>

                            <ul className="space-y-2">
                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>
                                        <strong>2023 г.</strong> – Бакалавър „Счетоводство и одит“
                                    </span>
                                </li>

                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>
                                        <strong>2024 г.</strong> – Магистър „Счетоводство и контрол“
                                    </span>
                                </li>
                            </ul>
                        </div>

                        <div className="mt-6 space-y-5">
                            <p className={textClass}>
                                Започнах работа като счетоводител преди средата на 2022 г., докато все още бях 3-ти курс. Исках още първата година да се откажа. Изпитвах съмнения, чувство че няма да се справя, че не е за мен.
                            </p>

                            <p className={textClass}>
                                Сега съм по-уверен, по-дисциплиниран и по-упорит от всякога.
                            </p>

                            <p className={textClass}>
                                Нито е нужно да си най-умния, най-богатия, най-силния.
                            </p>
                        </div>

                        <div className={sectionLine} />

                        <p className="mb-5 text-[15px] font-semibold leading-[2] text-[#1f2937]">
                            Над 3-годишен опит в счетоводството.
                        </p>

                        <div>
                            <p className="mb-4 text-[15px] font-medium leading-[2] text-[#4b5563]">
                                Работа с над 30 предприятия като:
                            </p>

                            <ul className="space-y-2">
                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>ИНОВАТИВНИ ФИНАНСИ ХОЛДИНГ</span>
                                </li>

                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>7К КОРПОРЕЙШЪН</span>
                                </li>

                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>С. Г. ГРУП</span>
                                </li>
                            </ul>
                        </div>

                        <div className="mt-6 space-y-5">
                            <p className={textClass}>
                                Притежавам повече от добри познания на кодекси, закони и счетоводни стандарти.
                            </p>
                            <p className={textClass}>
                                Винаги има място за подобрение.
                            </p>
                        </div>
                    </div>
                ) : (
                    <div className="mt-6 w-full max-w-[720px]">
                        <h1 className="mb-6 text-center text-[22px] font-bold text-[#1f2937]">
                            Nikola Halachev - Accountant
                        </h1>

                        <div className="space-y-5">
                            <p className={textClass}>
                                Every day I work hard and consistently, strengthening and improving the knowledge and skills I have built so far.
                            </p>

                            <p className={textClass}>
                                My goal is to reach as many people as possible who need accounting services and to help them.
                            </p>

                            <p className={textClass}>
                                I have accepted what I do as a mission and put everything into my development and the development of the people around me.
                            </p>

                            <p className={textClass}>
                                I am still building my name and reputation.
                            </p>
                        </div>

                        <div className={sectionLine} />

                        <div>
                            <p className="mb-4 text-[15px] font-medium leading-[2] text-[#4b5563]">
                                I obtained my professional qualification at the University of Economics – Varna:
                            </p>

                            <ul className="space-y-2">
                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>
                                        <strong>2023</strong> – Bachelor’s Degree in Accounting and Audit
                                    </span>
                                </li>

                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>
                                        <strong>2024</strong> – Master’s Degree in Accounting and Control
                                    </span>
                                </li>
                            </ul>
                        </div>

                        <div className="mt-6 space-y-5">
                            <p className={textClass}>
                                I started working as an accountant before mid-2022 while still a university student.
                            </p>

                            <p className={textClass}>
                                Now I am more confident, disciplined and persistent than ever.
                            </p>

                            <p className={textClass}>
                                You do not need to be the smartest, richest or strongest.
                            </p>
                        </div>

                        <div className={sectionLine} />

                        <p className="mb-5 text-[15px] font-semibold leading-[2] text-[#1f2937]">
                            Over 3 years of accounting experience.
                        </p>

                        <div>
                            <p className="mb-4 text-[15px] font-medium leading-[2] text-[#4b5563]">
                                Worked with more than 30 companies such as:
                            </p>

                            <ul className="space-y-2">
                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>INNOVATIVE FINANCE HOLDING</span>
                                </li>

                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>7K CORPORATION</span>
                                </li>

                                <li className="flex items-start gap-3 text-[15px] leading-[2] text-[#4b5563]">
                                    <span className={triangle}></span>
                                    <span>S. G. GROUP</span>
                                </li>
                            </ul>
                        </div>

                        <div className="mt-6 space-y-5">
                            <p className={textClass}>
                                I possess strong knowledge of laws, regulations and accounting standards.
                            </p>
                            <p className={textClass}>
                                There is always room for improvement.
                            </p>
                        </div>
                    </div>
                )}
            </div>
        </div>
    )
}