import { useEffect, useState } from "react"

type ServiceRequest = {
    id: number
    fullName: string
    email: string
    phoneNumber: string
    message: string
    offeredPrice: number
    status: string
    createdAt: string
    service?: {
        id: number
        title?: string
        name?: string
    } | null
}

export default function ServiceRequestsAdmin() {
    const [requests, setRequests] = useState<ServiceRequest[]>([])

    const load = () => {
        fetch("/api/admin/service-requests", {
            credentials: "include"
        })
            .then(r => r.json())
            .then(setRequests)
    }

    useEffect(() => {
        load()
    }, [])

    const remove = async (id: number) => {
        await fetch(`/api/admin/service-requests/${id}`, {
            method: "DELETE",
            credentials: "include"
        })

        load()
    }

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-6">Service Requests</h1>

            <div className="overflow-x-auto">
                <table className="w-full bg-white border rounded-xl shadow-sm overflow-hidden">
                    <thead className="bg-gray-50">
                        <tr>
                            <th className="p-3 text-left">ID</th>
                            <th className="p-3 text-left">Client</th>
                            <th className="p-3 text-left">Email</th>
                            <th className="p-3 text-left">Phone</th>
                            <th className="p-3 text-left">Service</th>
                            <th className="p-3 text-left">Price</th>
                            <th className="p-3 text-left">Status</th>
                            <th className="p-3 text-left">Message</th>
                            <th className="p-3 text-left">Actions</th>
                        </tr>
                    </thead>

                    <tbody>
                        {requests.map(r => (
                            <tr key={r.id} className="border-b last:border-b-0 hover:bg-gray-50 transition">
                                <td className="p-3 text-xs text-gray-500">{r.id}</td>
                                <td className="p-3">{r.fullName}</td>
                                <td className="p-3">{r.email}</td>
                                <td className="p-3">{r.phoneNumber}</td>
                                <td className="p-3">{r.service?.title ?? r.service?.name ?? "-"}</td>
                                <td className="p-3">{r.offeredPrice}</td>
                                <td className="p-3">{r.status}</td>
                                <td className="p-3 max-w-xs truncate" title={r.message}>{r.message}</td>
                                <td className="p-3">
                                    <button
                                        onClick={() => remove(r.id)}
                                        className="px-3 py-1.5 bg-red-600 text-white rounded-lg transition duration-200 hover:bg-red-700 hover:scale-105 active:scale-95"
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}