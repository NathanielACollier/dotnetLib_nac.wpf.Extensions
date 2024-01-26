using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;

namespace nac.wpf.Extensions;

public static class FlowDocumentExtension
{


    public static byte[] GetXPS(this FlowDocument doc, int width, int height)
    {
        MemoryStream stream = new MemoryStream();

        // create a package
        using (Package package = Package.Open(stream, FileMode.Create ))
        {
            // create an empty xps document
            using (XpsDocument xpsDoc = new XpsDocument(package, CompressionOption.Maximum))
            {

                // create a serialization manager
                XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                // retrieve document paginator
                DocumentPaginator paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;

                // set page size
                paginator.PageSize = new System.Windows.Size(width, height);
                // save as xps
                rsm.SaveAsXaml(paginator);
                rsm.Commit();

            }
        }

        return stream.ToArray();
    }

    public static FlowDocument AddParagraphs(this FlowDocument flowDoc, params Paragraph[] args)
    {
        foreach (Paragraph arg in args)
        {
            arg.Margin = new Thickness(0, 0, 0, 0); /* This will make sure the paragraphs stack one on top of the other with no spacing below them
                                                     * By default paragraphs have a margin that makes them double spaced between paragraphs */

            flowDoc.Blocks.Add(arg);
        }

        return flowDoc;
    }

    public static Paragraph AddInlines(this Paragraph p, params Inline[] args)
    {
        foreach (Inline arg in args)
        {
            p.Inlines.Add(arg);
        }

        return p;
    }




}
