# C# QR CodecGenerator Read QR with Machine Learning

Welcome to this exciting repository where we delve into the world of QR code generation in .NET MAUI, leveraging the robust capabilities of IronQR. This project is inspired by the innovative use of .NET MAUI and Syncfusion libraries for OCR and PDF manipulation, showcasing the versatility of .NET technologies in handling complex image processing tasks.

**Project Overview**
--------------------

This repository demonstrates the integration of [IronQR](https://ironsoftware.com/csharp/qr/tutorials/csharp-qr-code-generator/), a powerful QR code generation and reading library, within a .NET MAUI application. Similar to how the Syncfusion .NET MAUI library facilitates creating, reading, and editing PDF documents through OCR technology, this project emphasizes QR code functionalities in mobile applications.

### **Key Features**:

**IronQR Integration**: We explore the seamless integration of IronQR with .NET MAUI, enabling your applications to generate and read QR codes efficiently.

**High-Quality QR Code Processing**: IronQR ensures high accuracy and quality in [QR code generation and scanning.](https://ironsoftware.com/csharp/qr/examples/qr-quickstart/)

**Cross-Platform Functionality**: Emphasizing .NET MAUI's cross-platform capabilities, this project demonstrates how QR code features can be implemented consistently across different platforms.

**Objective**
-------------

The main goal of this project is to provide a comprehensive guide and working example for developers looking to incorporate QR code functionality in their .NET MAUI applications. By drawing parallels with the existing .NET MAUI OCR scanner application, this repository aims to highlight the adaptability and power of .NET technologies in modern mobile application development.

We invite you to explore, contribute, and innovate as we dive into the world of QR codes with .NET MAUI and IronQR.

Steps to Build A C# QR Code Generator Application in .NET MAUI
--------------------------------------------------------------

### Step 1: Download the Project

Download the project to a preferred location on your disk. Open the solution file using Visual Studio.

### Step 2: Rebuild the Project

Rebuild the solution in Visual Studio. This step ensures that all the required NuGet packages are installed correctly.

### Step 3: Add UI Elements

#### **ContentPage Root Element**

**ContentPage** serves as the foundation of our user interface. It encapsulates all other elements, providing a canvas for our application's layout.

```
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MAUI_QR_Generator.MainPage">
    <!-- Other elements go here -->
</ContentPage>

```

#### **VerticalStackLayout**

**VerticalStackLayout** is used to arrange child elements vertically. This layout is essential for creating an intuitive flow in the app's interface, allowing elements to be stacked in a user-friendly manner.

```
<VerticalStackLayout Spacing="15" Padding="20" HorizontalOptions="Center" VerticalOptions="Start">
    <!-- Child elements go here -->
</VerticalStackLayout>

```

#### **Label for Title**

Displays the application's title. Attributes include **FontSize**, **FontAttributes**, **HorizontalOptions**, **Margin**, and **FontFamily**.

```
<Label Text="🔳 QR Code Generator"
       FontSize="Large"
       FontAttributes="Bold"
       HorizontalOptions="Center"
       Margin="0,0,0,10"
       FontFamily="Arial" />

```

#### **Text Input Editor**

An **Editor** named **inputText** allows users to enter text. It has custom height, width, background color, and font settings.

```
<Editor x:Name="inputText"
        Placeholder="Enter text here"
        HeightRequest="100"
        WidthRequest="300"
        BackgroundColor="#F0F0F0"
        TextColor="Black"
        FontFamily="Arial" />

```

#### **Generate QR Code Button**

```
<Button x:Name="generateButton"
        Text="Generate"
        BackgroundColor="#4CAF50"
        TextColor="White"
        WidthRequest="150"
        Clicked="OnGenerateClicked"
        Style="{StaticResource ButtonStyle}" />

```

**QR Code Image Display**

The **Image** control is where the generated QR code is displayed. It's a crucial component that visually represents the outcome of the user's input.

```
<Image x:Name="qrCodeImage"
       HeightRequest="200"
       WidthRequest="200"
       HorizontalOptions="Center"
       VerticalOptions="CenterAndExpand" />

```

#### **Save QR Code Button**

```
<Button x:Name="saveButton"
        Text="Save"
        BackgroundColor="#4CAF50"
        TextColor="White"
        WidthRequest="150"
        Clicked="OnSaveClicked"
        Style="{StaticResource ButtonStyle}" />

```

#### **Resource Dictionary**

This section defines our buttons' styles and visual appearance, ensuring a consistent and appealing user interface.

```
<ContentPage.Resources>
    <ResourceDictionary>
        <Style x:Key="ButtonStyle" TargetType="Button">
            <Setter Property="BackgroundColor" Value="#4CAF50" />
            <Setter Property="TextColor" Value="White" />
            <Style.Triggers>
                <Trigger TargetType="Button" Property="IsPressed" Value="True">
                    <Setter Property="BackgroundColor" Value="#388E3C" />
                </Trigger>
            </Style.Triggers>
        </Style>
    </ResourceDictionary>
</ContentPage.Resources>

```

### Step 4: Backend Code

#### **MainPage Class Definition**

These namespaces are essential for the functionality of our application. They include references to the IronQr library, file handling, and MAUI controls.

```
using IronQr;
using System.IO;
using Microsoft.Maui.Controls;
using IronSoftware.Drawing;
using System.Drawing;
using CommunityToolkit.Maui.Storage;

```

#### **Constructor**

**MainPage** class is the backbone of our application's logic. It initializes the UI components and houses the event handlers for user interactions.

```
namespace MAUI_QR_Generator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    // Event handlers go here
}

```

#### **Generate QR Code Event Handler**

This method is triggered when the user clicks the 'Generate' button. It encapsulates the logic for creating a QR code based on the user's input.

```
private void OnGenerateClicked(object sender, EventArgs e)
{
    IronQr.License.LicenseKey = "Your-License-Key";
    QrCode myQr = QrWriter.Write(inputText.Text);
    AnyBitmap qrImage = myQr.Save();
    string tempFilePath = Path.Combine(FileSystem.CacheDirectory, "tempQrCode.png");
    qrImage.SaveAs(tempFilePath);
    qrCodeImage.Source = ImageSource.FromFile(tempFilePath);
}

```

#### **Save QR Code Event Handler**

This asynchronous method handles the saving of the generated QR code. It is an essential part of the application, enabling users to store their QR codes for future use.

```
private async void OnSaveClicked(object sender, EventArgs e)
{
    string tempFilePath = Path.Combine(FileSystem.CacheDirectory, "tempQrCode.png");
    if (File.Exists(tempFilePath))
    {
        using var stream = new MemoryStream(File.ReadAllBytes(tempFilePath));
        var fileSaverResult = await FileSaver.Default.SaveAsync("QrCode.png", stream, CancellationToken.None);
        if (fileSaverResult.IsSuccessful)
        {
            await DisplayAlert("Success", $"File is saved: {fileSaverResult.FilePath}", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Failed to save the file", "OK");
        }
    }
    else
    {
        await DisplayAlert("Error", "QR Code not found. Please generate it first.", "OK");
    }
}
```

**Running the Project**
-----------------------

Choose the Windows Machine as the target for deployment. Click on the 'Run' button or use the corresponding shortcut to start the application.

When the application is launched, you'll be presented with the following interface.

![](https://images.surferseo.art/18ee8941-daaf-454d-922c-b1d6fc820a81.png)

Now write the data that you want to put into the QR code in the Text box and hit the generate button. It'll generate the QR code and present you in the Image Box.

![](https://images.surferseo.art/2ec16dcd-9b92-4b73-ac8e-941191005ddc.png)

Now hit the Save button to save the QR code image file. Select the destination, give a name to the QR code, and hit the save button.

![](https://images.surferseo.art/bcc7d189-f114-410c-939d-68f2309144f8.png)

Once the QR code is saved, you'll see the following alert on your screen.

![](https://images.surferseo.art/47103f70-d2c9-44e2-9365-9949b393b3c7.png)

Here you can see the generated QR code image.

![](https://images.surferseo.art/45098293-3b3b-41dc-a47e-c075ee37b978.png)

This application exemplifies the power and versatility of .NET MAUI in creating functional and user-friendly mobile applications.

**Resources**
-------------

1.  **Product page**: [IronQR - C# QR Code Library](https://ironsoftware.com/csharp/qr/)

2.  **Documentation page**: [IronQR - .NET C#/VB QR Library](https://ironsoftware.com/csharp/qr/docs/)

3.  **Online demo**: IronQR provides online code examples, such as generating QR codes in C#. Code Example: [Generate a QR Code](https://ironsoftware.com/csharp/qr/examples/generate-qr-code/)

4.  Knowledge Base: IronQR's troubleshooting section serves as a knowledge base. Users can send code snippets and application details for support. [Engineering Request - IronQR](https://ironsoftware.com/csharp/qr/troubleshooting/engineering-request-qr/)

**Support and Feedback**
------------------------

For queries or support requests, users can send code snippets and additional application details to IronQR's support team through their Engineering Request page.

License
-------

IronQR adopts a dual-licensing model to accommodate various user needs. For development and testing, it offers a Free Commercial License, suitable for small projects or for those wanting to explore IronQR's features without financial commitment. In contrast, deploying IronQR in a commercial setting requires a Professional License, which also includes support and product upgrades.

The licensing terms are designed to be developer-friendly and straightforward, with flexibility to suit different project needs and budgets. Additionally, IronQR provides a free trial option, allowing users to evaluate the product before committing. For more detailed information on licensing, you can visit their [licensing page](https://ironsoftware.com/csharp/qr/licensing/).
