import { useEffect, useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Usuario = {
  usuarioID: number;
  nombre: string;
  passwordHash: string;
  idRol: number;
};

type Rol = {
  idRol: number;
  nombreRol: string;
};

const formFields = (
  roles: Rol[]
) => [
  {
    key: "nombre",
    label: "Nombre del usuario",
    placeholder: "Ingresa el nombre del Usuario",
  },
  {
    key: "idRol",
    label: "Rol del usuario",
    placeholder: "Selecciona el rol del usuario",
    type: "select",
    options: roles.map((role) => ({
      value: role.idRol.toString(),
      label: role.nombreRol,
    })),
  },
] satisfies { key: keyof Usuario; label: string; placeholder?: string; type?: string; options?: { value: string; label: string }[] }[];

const UsuarioPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [roles, setRoles] = useState<Rol[]>([]);

  const {
    data: datas = [],
    loading,
    error,
    refetch,
  } = useCallApi<Usuario[]>({
    url: "/api/usuario",
    methodType: "GET",
  });

  const {
    data: rolesData = [],
    loading: rolesLoading,
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    error: rolesError,
  } = useCallApi<Rol[]>({
    url: "/api/rol", 
    methodType: "GET",
  });

  useEffect(() => {
    if (rolesData) {
      setRoles(rolesData);
    }
  }, [rolesData]);

  const handleAdd = async (newItem: Omit<Usuario, "usuarioID">) => {
    try {
      setIsSubmitting(true);
      await callApi<Usuario, typeof newItem>({
        url: "/api/usuario",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar Usuario:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Usuario) => {
    try {
      setIsSubmitting(true);
      await callApi<Usuario, Usuario>({
        url: `/api/usuario/${updatedItem.usuarioID}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar Usuario:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Usuario) => {
    if (!confirm(`¿Eliminar "${item.nombre}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/usuario/${item.usuarioID}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar Usuario:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Usuario) => {
    console.log("Ver:", item);
  };

  if (error) return <div>Error al cargar datos</div>;
  if (rolesLoading || loading || isSubmitting) return <div>Cargando...</div>;

  if (!datas || datas.length === 0) {
    return (
      <CustomTable
        tableTitle="Usuarios"
        data={[]}
        columns={[]}
        formFields={formFields(roles)}
        onAdd={handleAdd}
        onEdit={handleEdit}
        onView={handleView}
        onDelete={handleDelete}
      />
    );
  }

  const columns = createDynamicColumns(datas);
  return (
    <CustomTable
      tableTitle="Usuarios"
      data={datas}
      columns={columns}
      formFields={formFields(roles)} 
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default UsuarioPageContainer;
