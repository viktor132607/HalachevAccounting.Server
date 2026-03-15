import { useEffect, useState } from "react"

type Request = {
    id: number
    name: string
    email: string
    phone: string
    subject: string
    message: string
    status: string
    adminComment: string
}

export default function ContactRequestsAdmin() {

    const [requests, setRequests] = useState<Request[]>([])

    const load = () => {
        fetch("/api/admin/contact-requests", {
            credentials: "include"
        })
            .then(r => r.json())
            .then(setRequests)
    }

    useEffect(() => {
        load()
    }, [])

    const changeStatus = async (id: number, status: string) => {
        await fetch(`/api/admin/contact-requests/${id}/status`, {
            method: "PUT",
            credentials: "include",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(status)
        })

        load()
    }

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-6">Contact Requests</h1>

            <table className="w-full bg-white border rounded-xl shadow-sm">
                <thead className="bg-gray-50">
                    <tr>
                        <th className="p-3 text-left">Name</th>
                        <th className="p-3 text-left">Email</th>
                        <th className="p-3 text-left">Subject</th>
                        <th className="p-3 text-left">Status</th>
                        <th className="p-3 text-left">Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {requests.map(r => (
                        <tr key={r.id} className="border-b hover:bg-gray-50 transition">
                            <td className="p-3">{r.name}</td>
                            <td className="p-3">{r.email}</td>
                            <td className="p-3">{r.subject}</td>
                            <td className="p-3">{r.status}</td>

                            <td className="p-3 flex gap-2">
                                <button
                                    onClick={() => changeStatus(r.id, "Completed")}
                                    className="px-3 py-1.5 bg-green-600 text-white rounded-lg hover:bg-green-700 hover:scale-105 transition"
                                >
                                    Complete
                                </button>

                                <button
                                    onClick={() => changeStatus(r.id, "InProgress")}
                                    className="px-3 py-1.5 bg-blue-600 text-white rounded-lg hover:bg-blue-700 hover:scale-105 transition"
                                >
                                    In Progress
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}