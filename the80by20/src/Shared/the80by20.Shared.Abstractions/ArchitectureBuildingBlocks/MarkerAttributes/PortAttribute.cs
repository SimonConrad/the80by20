namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

// conecpt of ports and andapters - clean architecture
// INFO Ports is name from  ports - adapters architecture; ports - interfaces that app layer needs to do its job
// implemented by adapters in infrastructure layer; this way we achive taht higher modules (in terms of logic, app)
// do not depned on lower modules (infrastruture); inversion of control is achieved
// - direction of flow of program execution is in opposite then direction of dependenciesis
// infrastruture.csproj references app.csproj (becouse it implements interfaces which app decideded how to look)
// however durng program execution controle flow is from app to infrastrture
// IoC is good practice - when you code concentrate on app, domain layer - they dicated the port shape then implement them in infrastrure layer
// todo add Port Adapter marker attrubute in code
public class PortAttribute : Attribute
{ }