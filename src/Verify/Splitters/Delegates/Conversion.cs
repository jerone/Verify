namespace VerifyTests;

public delegate ConversionResult Conversion<in T>(T target, ConversionContext context);

public delegate ConversionResult Conversion(object target, ConversionContext context);