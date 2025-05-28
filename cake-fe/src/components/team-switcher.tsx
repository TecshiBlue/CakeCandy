import {
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar"

import Cake from "../assets/cake.jpg"

const TeamSwitcher = () => {

  return (
    <SidebarMenu>
      <SidebarMenuItem>
            <SidebarMenuButton
              size="lg"
              className="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
            >
              <div className="bg-sidebar-accent flex aspect-square size-8 items-center justify-center rounded-lg">
                    <img
                        style={{
                          borderRadius: "30px"
                        }}
                        src={Cake}
                        alt={"lg"}/>
              </div>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <span className="truncate font-medium"> Cake Candy</span>
                <span className="truncate text-xs">Admin</span>
              </div>
            </SidebarMenuButton>
      </SidebarMenuItem>
    </SidebarMenu>
  )
}

export default TeamSwitcher;