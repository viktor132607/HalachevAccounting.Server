import { useEffect, useState } from "react"

type User = {
    id: string
    email: string
    isBlocked: boolean
}

export default function UsersAdmin() {
    const [users, setUsers] = useState<User[]>([])

    const loadUsers = () => {
        fetch("/api/admin/users", {
            credentials: "include"
        })
            .then(r => r.json())
            .then(data => setUsers(data))
    }

    useEffect(() => {
        loadUsers()
    }, [])

    const block = async (id: string) => {
        await fetch(`/api/admin/users/${id}/block`, {
            method: "POST",
            credentials: "include"
        })

        loadUsers()
    }

    const unblock = async (id: string) => {
        await fetch(`/api/admin/users/${id}/unblock`, {
            method: "POST",
            credentials: "include"
        })

        loadUsers()
    }

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-6">Users Admin</h1>

            <table className="w-full border bg-white rounded-xl overflow-hidden shadow-sm">
                <thead className="bg-gray-50">
                    <tr className="border-b">
                        <th className="text-left p-3">User ID</th>
                        <th className="text-left p-3">Email</th>
                        <th className="text-left p-3">Status</th>
                        <th className="text-left p-3">Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {users.map(u => (
                        <tr key={u.id} className="border-b last:border-b-0 hover:bg-gray-50 transition">
                            <td className="p-3 text-xs text-gray-500 break-all">{u.id}</td>

                            <td className="p-3">{u.email}</td>

                            <td className="p-3">
                                <span
                                    className={`inline-block px-3 py-1 rounded-full text-sm font-medium ${u.isBlocked
                                            ? "bg-red-100 text-red-700"
                                            : "bg-green-100 text-green-700"
                                        }`}
                                >
                                    {u.isBlocked ? "Blocked" : "Active"}
                                </span>
                            </td>

                            <td className="p-3 flex gap-2">
                                {!u.isBlocked && (
                                    <button
                                        onClick={() => block(u.id)}
                                        className="px-3 py-1.5 bg-red-600 text-white rounded-lg transition duration-200 hover:bg-red-700 hover:scale-105 active:scale-95"
                                    >
                                        Block
                                    </button>
                                )}

                                {u.isBlocked && (
                                    <button
                                        onClick={() => unblock(u.id)}
                                        className="px-3 py-1.5 bg-green-600 text-white rounded-lg transition duration-200 hover:bg-green-700 hover:scale-105 active:scale-95"
                                    >
                                        Unblock
                                    </button>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}