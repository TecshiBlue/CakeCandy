import { useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Cliente = {
  idCliente: number;
  nombreCliente: string;
  apellidoCliente: string;
  emailCliente: string;
  telefonoCliente: string;
};

const formFields = [
  {
    key: "nombreCliente",
    label: "Nombre del cliente",
    placeholder: "Ingresa el nombre del cliente",
  },
  {
    key: "apellidoCliente",
    label: "Apellido del cliente",
    placeholder: "Ingresa el apellido del cliente",
  },
  {
    key: "emailCliente",
    label: "Email del cliente",
    placeholder: "Ingresa el email del cliente",
    type: "email"
  },
  {
    key: "telefonoCliente",
    label: "Telefono del cliente",
    placeholder: "Ingresa el telefono del cliente",
  },
] satisfies { key: keyof Cliente; label: string; placeholder?: string, type?:string }[];


const ClientePageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const {
    data: datas = [], 
    loading,
    error,
    refetch,
  } = useCallApi<Cliente[]>({
    url: "/api/cliente",
    methodType: "GET",
  });

  const handleAdd = async (newItem: Omit<Cliente, "idCliente">) => {
    try {
      setIsSubmitting(true);
      await callApi<Cliente, typeof newItem>({
        url: "/api/cliente",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar la Cliente:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Cliente) => {
    try {
      setIsSubmitting(true);
      await callApi<Cliente, Cliente>({
        url: `/api/cliente/${updatedItem.idCliente}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar Cliente:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Cliente) => {
    if (!confirm(`¿Eliminar "${item.nombreCliente}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/cliente/${item.idCliente}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar la Cliente:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Cliente) => {
    console.log("Ver:", item);
  };

  if (error) return <div>Error al cargar datos</div>;
  if (loading || isSubmitting) return <div>Cargando...</div>;
  
  if (!datas || datas.length === 0) {
    return (
    <CustomTable
      tableTitle="Clientes"
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
      tableTitle="Clientes"
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

export default ClientePageContainer;
