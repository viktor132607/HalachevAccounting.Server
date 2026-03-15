import { Link } from "react-router-dom"
import { useEffect, useState } from "react"

export default function AdminPanel() {
    const [stats, setStats] = useState({
        users: 0,
        contacts: 0,
        serviceRequests: 0,
        blogPosts: 0
    })

    useEffect(() => {
        fetch("/api/admin/dashboard", {
            credentials: "include"
        })
            .then(r => r.json())
            .then(data => setStats(data))
    }, [])

    return (
        <div className="min-h-screen bg-gray-100 py-10 px-4">
            <div className="max-w-5xl mx-auto">
                <h1 className="text-3xl font-bold mb-8">Admin Panel</h1>

                <div className="grid grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
                    <div className="bg-white rounded-lg shadow p-4 border">
                        <h3 className="text-sm text-gray-500">Users</h3>
                        <p className="text-2xl font-bold">{stats.users}</p>
                    </div>

                    <div className="bg-white rounded-lg shadow p-4 border">
                        <h3 className="text-sm text-gray-500">Contact Requests</h3>
                        <p className="text-2xl font-bold">{stats.contacts}</p>
                    </div>

                    <div className="bg-white rounded-lg shadow p-4 border">
                        <h3 className="text-sm text-gray-500">Service Requests</h3>
                        <p className="text-2xl font-bold">{stats.serviceRequests}</p>
                    </div>

                    <div className="bg-white rounded-lg shadow p-4 border">
                        <h3 className="text-sm text-gray-500">Blog Posts</h3>
                        <p className="text-2xl font-bold">{stats.blogPosts}</p>
                    </div>
                </div>

                <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
                    <Link
                        to="/admin/blog"
                        className="bg-white rounded-lg shadow p-6 border hover:shadow-lg hover:-translate-y-0.5 transition"
                    >
                        <h2 className="text-xl font-semibold mb-2">Blog</h2>
                        <p className="text-gray-600 text-sm">Manage blog posts</p>
                    </Link>

                    <Link
                        to="/admin/users"
                        className="bg-white rounded-lg shadow p-6 border hover:shadow-lg hover:-translate-y-0.5 transition"
                    >
                        <h2 className="text-xl font-semibold mb-2">Users</h2>
                        <p className="text-gray-600 text-sm">Manage users</p>
                    </Link>

                    <Link
                        to="/admin/contact-requests"
                        className="bg-white rounded-lg shadow p-6 border hover:shadow-lg hover:-translate-y-0.5 transition"
                    >
                        <h2 className="text-xl font-semibold mb-2">Contact Requests</h2>
                        <p className="text-gray-600 text-sm">Manage contact messages</p>
                    </Link>

                    <Link
                        to="/admin/service-requests"
                        className="bg-white rounded-lg shadow p-6 border hover:shadow-lg hover:-translate-y-0.5 transition"
                    >
                        <h2 className="text-xl font-semibold mb-2">Service Requests</h2>
                        <p className="text-gray-600 text-sm">Manage client inquiries</p>
                    </Link>
                </div>
            </div>
        </div>
    )
}