import  UserDetails  from "./UserDetails"

const UserPageContainer = () => {
    const mockUser = {
        name: "Juan",
        lastname: "Pérez",
        username: "juanperez",
        password: "supersecreto123"
    }

    return (
        <main className="min-h-screen flex items-center justify-center p-4">
            <UserDetails {...mockUser} />
        </main>
    )
}

export default UserPageContainer;