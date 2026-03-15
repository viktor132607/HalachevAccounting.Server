import { Link, useLocation } from "react-router-dom"
import { useTranslation } from "react-i18next"
import { useAuth } from "../context/AuthContext"

export default function Navbar() {
    const { i18n } = useTranslation()
    const { user, isAdmin, logout, loading } = useAuth()
    const location = useLocation()

    const isBg = i18n.language?.toLowerCase().startsWith("bg")

    const leftItems = isBg
        ? [
            { to: "/", label: "Начало" },
            { to: "/services", label: "Услуги" },
        ]
        : [
            { to: "/", label: "Home" },
            { to: "/services", label: "Services" },
        ]

    const rightItems = isBg
        ? [
            { to: "/about", label: "За мен" },
            { to: "/contact", label: "Контакти" },
            { to: "/blog", label: "Блог" },
        ]
        : [
            { to: "/about", label: "About Me" },
            { to: "/contact", label: "Contact" },
            { to: "/blog", label: "Blog" },
        ]

    const linkClass = (to: string) =>
        `relative text-[14px] font-semibold tracking-[0.01em] transition ${location.pathname === to
            ? "text-slate-950 after:absolute after:-bottom-[14px] after:left-0 after:h-[2px] after:w-full after:bg-slate-950"
            : "text-slate-600 hover:text-slate-950"
        }`

    const authClass =
        "text-[11px] font-semibold uppercase tracking-[0.14em] text-slate-600 transition hover:text-slate-950"

    const langButtonClass = (active: boolean) =>
        `text-[11px] font-bold uppercase tracking-[0.14em] transition ${active ? "text-slate-950" : "text-slate-400 hover:text-slate-700"
        }`

    return (
        <header className="sticky top-0 z-50 border-b border-slate-200 bg-white">
            <div className="mx-auto max-w-[1400px] px-6">
                <div className="relative flex min-h-[92px] items-center justify-center">
                    <div className="flex items-center gap-8">
                        {leftItems.map((item) => (
                            <Link key={item.to} to={item.to} className={linkClass(item.to)}>
                                {item.label}
                            </Link>
                        ))}
                    </div>

                    <div className="mx-20 flex shrink-0 justify-center">
                        <Link to="/">
                            <img
                                src="/images/mainlogo.png"
                                alt="Halachev Accounting"
                                className="h-14 w-auto object-contain"
                            />
                        </Link>
                    </div>

                    <div className="flex items-center gap-8">
                        {rightItems.map((item) => (
                            <Link key={item.to} to={item.to} className={linkClass(item.to)}>
                                {item.label}
                            </Link>
                        ))}
                    </div>

                    <div className="absolute right-0 top-1/2 flex -translate-y-1/2 items-center gap-5">
                        {!loading && !user && (
                            <>
                                <Link to="/identity/login" className={authClass}>
                                    {isBg ? "Вход" : "Login"}
                                </Link>

                                <Link to="/identity/register" className={authClass}>
                                    {isBg ? "Регистрация" : "Register"}
                                </Link>
                            </>
                        )}

                        {!loading && user && (
                            <>
                                <Link to="/identity/profile" className={authClass}>
                                    {isBg ? "Профил" : "Profile"}
                                </Link>

                                {isAdmin && (
                                    <Link to="/admin" className={authClass}>
                                        Admin
                                    </Link>
                                )}

                                <button type="button" onClick={() => void logout()} className={authClass}>
                                    {isBg ? "Изход" : "Logout"}
                                </button>
                            </>
                        )}

                        <div className="flex items-center gap-2">
                            <button
                                type="button"
                                onClick={() => void i18n.changeLanguage("bg")}
                                className={langButtonClass(isBg)}
                            >
                                BG
                            </button>

                            <span className="text-slate-300">|</span>

                            <button
                                type="button"
                                onClick={() => void i18n.changeLanguage("en")}
                                className={langButtonClass(!isBg)}
                            >
                                EN
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </header>
    )
}