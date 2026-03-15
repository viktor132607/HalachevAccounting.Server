import { Link } from "react-router-dom"
import { Facebook, Instagram, Mail, Phone } from "lucide-react"
import { useTranslation } from "react-i18next"

export default function Footer() {
    const { i18n } = useTranslation()
    const isBg = i18n.language?.toLowerCase().startsWith("bg")

    const t = isBg
        ? {
            description: "Професионално счетоводно обслужване и консултации. Налични 24/7.",
            company: "Фирма",
            info: "Информация",
            home: "Начало",
            about: "За мен",
            contact: "Контакти",
            privacy: "Политика за поверителност",
            terms: "Общи условия",
            cookies: "Политика за бисквитките",
            rights: "Всички права запазени.",
        }
        : {
            description: "Professional accounting & consulting services. Available 24/7.",
            company: "Company",
            info: "Legal",
            home: "Home",
            about: "About Me",
            contact: "Contact",
            privacy: "Privacy Policy",
            terms: "Terms of Service",
            cookies: "Cookie Policy",
            rights: "All rights reserved.",
        }

    return (
        <footer className="mt-16 border-t border-slate-200 bg-white dark:border-white/10 dark:bg-slate-950">
            <div className="mx-auto max-w-7xl px-6 py-10">
                <div className="flex flex-col gap-10 lg:flex-row lg:justify-between">
                    <div className="max-w-sm">
                        <Link to="/" className="inline-flex items-center">
                            <img
                                src="/images/mainlogo.png"
                                alt="Halachev Accounting"
                                className="h-12 w-auto object-contain"
                            />
                        </Link>

                        <p className="mt-4 text-sm leading-6 text-slate-600 dark:text-slate-300">
                            {t.description}
                        </p>

                        <div className="mt-5 flex items-center gap-3">
                            <a
                                href="https://facebook.com"
                                target="_blank"
                                rel="noopener noreferrer"
                                aria-label="Facebook"
                                className="inline-flex h-10 w-10 items-center justify-center rounded-full border border-slate-200 text-slate-700 transition hover:bg-slate-50 dark:border-white/10 dark:text-slate-200 dark:hover:bg-white/5"
                            >
                                <Facebook size={18} />
                            </a>

                            <a
                                href="https://instagram.com"
                                target="_blank"
                                rel="noopener noreferrer"
                                aria-label="Instagram"
                                className="inline-flex h-10 w-10 items-center justify-center rounded-full border border-slate-200 text-slate-700 transition hover:bg-slate-50 dark:border-white/10 dark:text-slate-200 dark:hover:bg-white/5"
                            >
                                <Instagram size={18} />
                            </a>
                        </div>
                    </div>

                    <div className="grid grid-cols-1 gap-10 sm:grid-cols-2">
                        <div className="min-w-[160px]">
                            <h4 className="mb-3 text-sm font-semibold text-slate-900 dark:text-white">
                                {t.company}
                            </h4>

                            <div className="flex flex-col gap-2 text-sm text-slate-600 dark:text-slate-300">
                                <Link to="/" className="transition hover:text-slate-950 dark:hover:text-white">
                                    {t.home}
                                </Link>
                                <Link to="/about" className="transition hover:text-slate-950 dark:hover:text-white">
                                    {t.about}
                                </Link>
                                <Link to="/contact" className="transition hover:text-slate-950 dark:hover:text-white">
                                    {t.contact}
                                </Link>
                            </div>
                        </div>

                        <div className="min-w-[180px]">
                            <h4 className="mb-3 text-sm font-semibold text-slate-900 dark:text-white">
                                {t.info}
                            </h4>

                            <div className="flex flex-col gap-2 text-sm text-slate-600 dark:text-slate-300">
                                <Link to="/privacy" className="transition hover:text-slate-950 dark:hover:text-white">
                                    {t.privacy}
                                </Link>
                                <Link to="/terms" className="transition hover:text-slate-950 dark:hover:text-white">
                                    {t.terms}
                                </Link>
                                <Link to="/cookies" className="transition hover:text-slate-950 dark:hover:text-white">
                                    {t.cookies}
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="mt-8 flex flex-col gap-4 border-t border-slate-200 pt-6 text-sm text-slate-500 dark:border-white/10 dark:text-slate-400 md:flex-row md:items-center md:justify-between">
                    <p>
                        © {new Date().getFullYear()} Halachev Accounting. {t.rights}
                    </p>

                    <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:gap-5">
                        <a
                            href="tel:+359887764200"
                            className="inline-flex items-center gap-2 transition hover:text-slate-900 dark:hover:text-white"
                        >
                            <Phone size={16} />
                            <span>088 776 4200</span>
                        </a>

                        <a
                            href="mailto:contact@halachevaccounting.com"
                            className="inline-flex items-center gap-2 transition hover:text-slate-900 dark:hover:text-white"
                        >
                            <Mail size={16} />
                            <span>contact@halachevaccounting.com</span>
                        </a>
                    </div>
                </div>
            </div>
        </footer>
    )
}