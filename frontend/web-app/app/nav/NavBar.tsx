import { AiOutlineCar } from "react-icons/ai";

export default function NavBar() {
    return (
        <header className="sticky top-0 z-50 flex items-center justify-between bg-white-800 p-4 text-gray-800 shadow-md">
            <div className="flex items-center gap-2 text-3xl font-semibold text-red-500">
                <AiOutlineCar size={34} />
                <div>Bid My Ride Auctions</div>
            </div>
            <div>Search</div>
            <div>Login</div>
        </header>
    )

}