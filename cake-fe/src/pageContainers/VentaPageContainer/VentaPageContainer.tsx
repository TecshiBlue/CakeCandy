import { useEffect, useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Venta = {
  idVenta: number;
  fechaVenta: string;
  idCliente: number;
  usuarioID: number;
};

type Usuario = {
  usuarioID: number;
  nombre: string;
  passwordHash: string;
  idRol: number;
};

type Cliente = {
  idCliente: number;
  nombreCliente: string;
  apellidoCliente: string;
  emailCliente: string;
  telefonoCliente: string;
};

const formFields = (
  clientes: Cliente[],
  usuarios: Usuario[]
) => [
  {
    key: "idCliente" as const,
    label: "Cliente",
    placeholder: "Selecciona el cliente",
    type: "select",
    options: clientes.map((cliente) => ({
      value: cliente.idCliente.toString(),
      label: `${cliente.nombreCliente} ${cliente.apellidoCliente}`,
    })),
  },
  {
    key: "usuarioID" as const,
    label: "Usuario",
    placeholder: "Selecciona el usuario que generó la venta",
    type: "select",
    options: usuarios.map((usuario) => ({
      value: usuario.usuarioID.toString(),
      label: usuario.nombre,
    })),
  },
] satisfies { 
  key: keyof Venta; 
  label: string; 
  placeholder?: string; 
  type?: string; 
  options?: { value: string; label: string }[] 
}[];

const VentaPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [clientes, setClientes] = useState<Cliente[]>([]);
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);

  const {
    data: datas = [], 
    loading,
    error,
    refetch,
  } = useCallApi<Venta[]>({
    url: "/api/venta",
    methodType: "GET",
  });

  const {
    data: clientesData = [],
    loading: clientesLoading,
  } = useCallApi<Cliente[]>({
    url: "/api/cliente",
    methodType: "GET",
  });

  const {
    data: usuariosData = [],
    loading: usuariosLoading,
  } = useCallApi<Usuario[]>({
    url: "/api/usuario",
    methodType: "GET",
  });

  useEffect(() => {
    if (clientesData) {
      setClientes(clientesData);
    }
  }, [clientesData]);

  useEffect(() => {
    if (usuariosData) {
      setUsuarios(usuariosData);
    }
  }, [usuariosData]);

  const handleAdd = async (newItemRaw: Omit<Venta, "idVenta">) => {
    try {
      setIsSubmitting(true);
      console.log("Agregando nueva venta:", newItemRaw);
  

      const fechaActual = new Date().toISOString();
  
      const newItem = {
        ...newItemRaw,
        fechaVenta: fechaActual,
        idCliente: Number(newItemRaw.idCliente),
        usuarioID: Number(newItemRaw.usuarioID),
      };
  
      await callApi<Venta, typeof newItem>({
        url: "/api/venta",
        methodType: "POST",
        body: newItem,
      });
  
      await refetch();
    } catch (error) {
      console.error("Error al agregar la Venta:", error);
      alert("Error al agregar: " + (error as Error).message);
    } finally {
      setIsSubmitting(false);
    }
  };
  

  const handleEdit = async (updatedItemRaw: Venta) => {
    try {
      setIsSubmitting(true);
      console.log("Editando venta:", updatedItemRaw);
      
      // Process numeric fields
      const updatedItem = { ...updatedItemRaw };
      // Convert string values from select inputs to numbers
      ["idCliente", "usuarioID"].forEach(field => {
        updatedItem[field as keyof typeof updatedItem] = Number(updatedItem[field as keyof typeof updatedItem]);
      });

      await callApi<Venta, Venta>({
        url: `/api/venta/${updatedItem.idVenta}`,
        methodType: "PUT",
        body: updatedItem,
      });
      
      await refetch();
    } catch (error) {
      console.error("Error al editar Venta:", error);
      alert("Error al editar: " + (error as Error).message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Venta) => {
    if (!confirm(`¿Eliminar la venta #${item.idVenta}?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/venta/${item.idVenta}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar la Venta:", error);
      alert("Error al eliminar: " + (error as Error).message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Venta) => {
    console.log("Ver:", item);
    
    // Find the related data to display readable information
    const cliente = clientes.find(c => c.idCliente === item.idCliente);
    const usuario = usuarios.find(u => u.usuarioID === item.usuarioID);
    
    // Format date for better readability
    const fecha = new Date(item.fechaVenta).toLocaleString();
    
    alert(
      `Detalles de Venta #${item.idVenta}\n\n` +
      `Fecha: ${fecha}\n` +
      `Cliente: ${cliente ? `${cliente.nombreCliente} ${cliente.apellidoCliente}` : 'Desconocido'}\n` +
      `Teléfono Cliente: ${cliente ? cliente.telefonoCliente : 'N/A'}\n` +
      `Email Cliente: ${cliente ? cliente.emailCliente : 'N/A'}\n` +
      `Usuario que registró: ${usuario ? usuario.nombre : 'Desconocido'}`
    );
  };

  const isReferenceDataLoading = clientesLoading || usuariosLoading;

  if (error) return <div className="p-8 text-red-500">Error al cargar datos: {String(error)}</div>;
  if (loading || isSubmitting || isReferenceDataLoading) return <div className="p-8">Cargando...</div>;

  if (!datas || datas.length === 0) {
    return (
      <CustomTable
        tableTitle="Ventas"
        data={[]}
        columns={[]}
        formFields={formFields(clientes, usuarios)}
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
      tableTitle="Ventas"
      data={datas}
      columns={columns}
      formFields={formFields(clientes, usuarios)}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default VentaPageContainer;