import { useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Rol = {
  idRol: number;
  nombreRol: string;
};

const formFields = [
  {
    key: "nombreRol",
    label: "Nombre del Rol",
    placeholder: "Ingrese Rol",
  },
] satisfies { key: keyof Rol; label: string; placeholder?: string, type?:string }[];


const RolPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const {
    data: datas = [], 
    loading,
    error,
    refetch,
  } = useCallApi<Rol[]>({
    url: "/api/rol",
    methodType: "GET",
  });

  const handleAdd = async (newItem: Omit<Rol, "idRol">) => {
    try {
      setIsSubmitting(true);
      await callApi<Rol, typeof newItem>({
        url: "/api/rol",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar Rol:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Rol) => {
    try {
      setIsSubmitting(true);
      await callApi<Rol, Rol>({
        url: `/api/rol/${updatedItem.idRol}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar Rol:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Rol) => {
    if (!confirm(`¿Eliminar "${item.nombreRol}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/rol/${item.idRol}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar la Rol:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Rol) => {
    console.log("Ver:", item);
  };

  if (error) return <div>Error al cargar datos</div>;
  if (loading || isSubmitting) return <div>Cargando...</div>;

  if (!datas || datas.length === 0) {
    return (
    <CustomTable
      tableTitle="Roles"
        data={[]}
        columns={[]}
      formFields={formFields}
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
      tableTitle="Roles"
      data={datas} 
      columns={columns}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default RolPageContainer;
