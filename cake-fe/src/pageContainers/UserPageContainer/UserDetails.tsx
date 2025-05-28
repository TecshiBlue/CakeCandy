"use client"

import { useState, useEffect } from "react"
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { Eye, EyeOff, Pencil } from "lucide-react"

interface UserDetailsProps {
    name: string
    lastname: string
    username: string
    password: string
}

const UserDetails = (initialUser: UserDetailsProps)=> {
    const [user, setUser] = useState(initialUser)
    const [originalUser, setOriginalUser] = useState(initialUser)
    const [editingField, setEditingField] = useState<string | null>(null)
    const [showPassword, setShowPassword] = useState(false)
    const [hasChanges, setHasChanges] = useState(false)

    useEffect(() => {
        setHasChanges(JSON.stringify(user) !== JSON.stringify(originalUser))
    }, [user, originalUser])

    const handleChange = (field: keyof UserDetailsProps, value: string) => {
        setUser((prev) => ({ ...prev, [field]: value }))
    }

    const handleSave = () => {
        console.log("Datos actualizados:", user)
        setOriginalUser(user)
        setEditingField(null)
    }

    const renderField = (
        label: string,
        field: keyof UserDetailsProps,
        isPassword?: boolean
    ) => {
        const value = user[field]

        const isEditing = editingField === field
        const displayValue = isPassword && !showPassword ? "*******" : value

        return (
            <div className="flex items-center justify-between py-2 gap-4">
                <div className="flex-1">
                    <p className="text-sm text-muted-foreground mb-1">{label}</p>
                    {isEditing ? (
                        <Input
                            type={isPassword && !showPassword ? "password" : "text"}
                            value={value}
                            onChange={(e) => handleChange(field, e.target.value)}
                        />
                    ) : (
                        <p className="text-base">{displayValue}</p>
                    )}
                </div>
                <div className="flex items-center gap-2">
                    {isPassword && !isEditing && (
                        <Button
                            variant="ghost"
                            size="icon"
                            onClick={() => setShowPassword((prev) => !prev)}
                        >
                            {showPassword ? <EyeOff className="w-4 h-4" /> : <Eye className="w-4 h-4" />}
                        </Button>
                    )}
                    <Button
                        variant="outline"
                        size="sm"
                        onClick={() => {
                            setEditingField(isEditing ? null : field)
                        }}
                    >
                        <Pencil className="w-4 h-4 mr-1" />
                        {isEditing ? "Cancelar" : "Editar"}
                    </Button>
                </div>
            </div>
        )
    }

    return (
        <Card className="max-w-md w-full">
            <CardHeader>
                <CardTitle>Detalles del Usuario</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
                {renderField("Nombre", "name")}
                {renderField("Apellido", "lastname")}
                {renderField("Usuario", "username")}
                {renderField("Contraseña", "password", true)}

                {hasChanges && (
                    <Button onClick={handleSave} className="mt-4 w-full">
                        Guardar cambios
                    </Button>
                )}
            </CardContent>
        </Card>
    )
}
export default UserDetails;
